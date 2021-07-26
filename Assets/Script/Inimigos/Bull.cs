using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull : MonoBehaviour
{   
    public enum BullState
    {
        PATROL, RUN, STUN, DEAD, FOLLOW
    }

    private Rigidbody2D rb;

    public BullState currentState;
    public ParticleSystem chargeParticle;
    public float lookDistance;
    public float walkSpeed;
    public float chargeSpeed;
    public bool isRevived;
    public bool isLookLeft;
    public Transform[] wayPoints;
    private Transform target;
    private int idWayPoint;

    [Header("RayCast")]
    public LayerMask raycastLayers;
    private int side;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = wayPoints[0];
    }

    private void Update()
    {
        switch (currentState)
        {
            case BullState.PATROL:
                Patrol();
            break;
            
            case BullState.RUN:
                Run();
            break;
        }
    }

    void CheckRayCast()
    {
        if(isLookLeft) { side = 1; } else { side = -1;}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * side, lookDistance, raycastLayers);
        Debug.DrawRay(transform.position, transform.right * side * lookDistance, Color.red, 0.2f);

        if(hit.collider != null)
        {
            print(hit.collider.gameObject.name);

            if(hit.collider.gameObject.tag == "Player" && currentState != BullState.RUN)
            {
                ChangeState(BullState.RUN);
                chargeParticle.Play();
            }
        }   
    }

    void Patrol()
    {
        target.position = new Vector3(wayPoints[idWayPoint].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime);
        if(transform.position == target.position)
        {
            idWayPoint++;
            if(idWayPoint >= wayPoints.Length)
            {
                idWayPoint = 0;
            }
            target = wayPoints[idWayPoint];
        }

        if(transform.position.x > target.position.x && isLookLeft)
        {
            Flip();
        }
        else if(transform.position.x < target.position.x && !isLookLeft)
        {
            Flip();
        }
    }

    void Run()
    {
        if(isLookLeft) { side = 1; } else { side = -1;}
        transform.position += transform.right * side * chargeSpeed * Time.deltaTime;
    }

    void Dead()
    {
        ChangeState(BullState.DEAD);
        //animacao de morte
        //indicador que pode ser revivido
    }

    void RealDead()
    {

    }

    void Flip()
    {
        isLookLeft = !isLookLeft;
        
        float s = transform.localScale.x *-1;
        Vector3 newScale = new Vector3(s, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }

    void ChangeState(BullState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case BullState.STUN:
                StopCoroutine(Delay(BullState.PATROL, 2.5f));
                StartCoroutine(Delay(BullState.PATROL, 2.5f));
            break;
        }
    }

    IEnumerator Delay(BullState nextState, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeState(nextState);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
                if(currentState == BullState.RUN)
                {
                    ChangeState(BullState.STUN);
                }

            break;

            case "DestructableWall":

            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PlayerHit":
                if(currentState != BullState.DEAD)
                {   
                    Dead();
                }

            break;

            
        }    
    }

    private void OnTriggerStay2D(Collider2D other) {
        
        switch(other.gameObject.tag)
        {
            case "Player":
                CheckRayCast();
            break;
        }
        
    }
}
