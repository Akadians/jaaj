using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatState
{
    PATROL, AGRESSIVE, DEAD
}


public class Bat : MonoBehaviour, ISkill
{
    [HideInInspector]public EnemyBehaviour behaviour;
    private Rigidbody2D rb;
    private Animator Anim;

    public BatState currentState;
    public bool isDetectedPlayer;
    public float shotSpeed;
    public Transform gunPoint;
    [HideInInspector]public Transform playerTransform;
    public GameObject shotPrefab;
    public float timeAttack;
    private bool isAttack;
    public bool isRandomMoviment;
    public int changeAnimation;

    void Start()
    {
        behaviour = GetComponent<EnemyBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(currentState == BatState.DEAD) 
        {
            Anim.SetInteger("State", 2);
            return;
        }
        ControlState();

        switch(currentState)
        {
            case BatState.PATROL:
                behaviour.FlyPatrol(isRandomMoviment);
            break;

            case BatState.AGRESSIVE:
                GunRotation();
                behaviour.ControlFlip(playerTransform);
            break;
        }
        AnimationChanger();
    }

    void GunRotation()
    {
        Vector2 direction = (Vector2)(transform.position - playerTransform.position);
        gunPoint.transform.right = -direction;
    }

    void ControlState()
    {
        if(isDetectedPlayer && currentState != BatState.AGRESSIVE)
        {
            ChangeState(BatState.AGRESSIVE);
        }
        else if(!isDetectedPlayer && currentState != BatState.PATROL)
        {
            ChangeState(BatState.PATROL);
        }

        if(currentState == BatState.AGRESSIVE && !isAttack)
        {
            StartCoroutine(DelaySkill());
        }
    }

    public void ResetPlayerDetection()
    {
        playerTransform = null;
        isDetectedPlayer = false;
    }

    public void Skill()
    {
        if(playerTransform != null)
        {            
            GameObject temp = Instantiate(shotPrefab, gunPoint.position, Quaternion.identity);
            temp.transform.right = gunPoint.right;
            temp.GetComponent<Shot>().SetBulletSpeed(shotSpeed);
        }
        
    }
    IEnumerator DelaySkill()
    {
        isAttack = true;
        Skill();
        yield return new WaitForSeconds(timeAttack);
        isAttack = false;
    }

    public void ChangeState(BatState newState)
    {
        if(currentState == BatState.DEAD) {return;}
        currentState = newState;

        switch(currentState)
        {
            case BatState.AGRESSIVE:
                GunRotation();
                if(!isAttack)
                {
                    StartCoroutine(DelaySkill());
                }
            break;

            case BatState.DEAD:
                behaviour.Dead();
                Dead();
            break;

            
        }
    }

    void Dead()
    {
        rb.gravityScale = 1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlayerHit" && currentState != BatState.DEAD)
        {
            ChangeState(BatState.DEAD);
        }
        
        if(other.gameObject.layer == 11 && currentState == BatState.DEAD)
        {
            //animacao de caido no chao
        }
    }
    void AnimationChanger()
    {
        changeAnimation = ((int)currentState);
        Anim.SetInteger("State", changeAnimation);
    }
}
