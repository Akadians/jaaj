using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public enum CatState
    {
        PATROL, RAGE, WAIT, DEAD
    }
    public CatState currentState;
    public float timeDelayToTeleport = 1f;
    public float waitTime = 3f;
    public float offsetY = 3f;
    public float downMass = 30f;
    public bool isTeleportedBack = true; //teleportou para o ponto de patrulha    

    [Header("Ground Check")]
    public LayerMask floorLayer;
    public Transform groundCheckA;
    public Transform groundCheckB;

    [Header("Bools")]
    public bool isTouchInGround;
    public bool isLookToPlayer;
    public bool isBacking;

    private Rigidbody2D rb;
    private Vector2 target = Vector2.zero;
    private Transform player;
    private EnemyBehaviour behaviour;
    private Animator Anim;
    private float startMass;
    private int changeAnimation;


    private void Start() 
    {
        behaviour = GetComponent<EnemyBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        startMass = rb.mass;
    }

    void FixedUpdate()
    {
        if(currentState == CatState.DEAD)
        {
            Anim.SetInteger("State", 3);
            return;
        }
        isTouchInGround = Physics2D.OverlapArea(groundCheckA.position, groundCheckB.position, floorLayer);        
    }

    private void Update()
    {
        if(currentState == CatState.DEAD) {return;}
        switch(currentState)
        {
            case CatState.PATROL:

                if(isTeleportedBack)
                {
                    behaviour.Patrol();
                }
                break;

            case CatState.WAIT:
                    if(player == null) {return;}
                    behaviour.ControlFlip(player);
                break; 
        }
        AnimationChanger();
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        if(!isLookToPlayer && isTouchInGround)
        {
            ChangeState(CatState.PATROL);
        }
    }

    void ChangeState(CatState newState)
    {
        if(currentState == CatState.DEAD) {return;}
        if(!isBacking)
        {
            currentState = newState;
        }

        switch (currentState)
        {
            case CatState.PATROL:
                if(!isBacking)
                {
                    rb.mass = startMass;
                    StartCoroutine(ResetPosition());
                }

            break;

            case CatState.WAIT:
                isTeleportedBack = false;
                StartCoroutine(WaitTime());
            break;

            case CatState.RAGE:
                rb.mass = downMass;
                isTeleportedBack = false;
                StopCoroutine(StartAttack());
                StartCoroutine(StartAttack());
            break;

            case CatState.DEAD:
                behaviour.Dead();
            break;
        }

        if(isLookToPlayer)
        {
            StopCoroutine(WaitTime());
        }
    }

    IEnumerator ResetPosition()
    {
        isBacking = true;
        yield return new WaitForSeconds(timeDelayToTeleport);
        int rand = Random.Range(0, behaviour.wayPoints.Length);
        transform.position = behaviour.wayPoints[rand].position;
        isTeleportedBack = true;
        isBacking = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(timeDelayToTeleport);
        transform.position = target;
    }

    IEnumerator WaitTouchGround()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => isTouchInGround);
        if(currentState != CatState.WAIT)
        {
            ChangeState(CatState.WAIT);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(currentState == CatState.DEAD) {return;}
        switch(other.gameObject.tag)
        {
            case "Player":
                if(!isTouchInGround) {StopAllCoroutines(); return;}
                RaycastHit2D hit = behaviour.CheckRayCastToPosition(other.transform.position);
                player = hit.transform;

                if(hit.collider != null)
                { 
                    if(hit.collider.gameObject.tag == "Player" && isTouchInGround)
                    {
                        isLookToPlayer = true;
                        if(currentState != CatState.RAGE)
                        {
                            StopCoroutine(StartAttack());
                            ChangeState(CatState.RAGE);
                        }

                        Vector2 hitPos = hit.transform.position;
                        target = new Vector2(hitPos.x, hitPos.y + offsetY);
                    }
                    else
                    {
                        isLookToPlayer = false;
                    }
                }
                else
                {
                    isLookToPlayer = false;
                }

            break;
        }        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && currentState == CatState.RAGE)
        {
            isLookToPlayer = false;
            StartCoroutine(WaitTouchGround());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PlayerHit":
                if(currentState != CatState.DEAD)
                {
                    ChangeState(CatState.DEAD);
                    Destroy(other.gameObject);
                }
            break;
        }
    }
    void AnimationChanger()
    {
        changeAnimation = ((int)currentState);
        Anim.SetInteger("State", changeAnimation);
    }
}
