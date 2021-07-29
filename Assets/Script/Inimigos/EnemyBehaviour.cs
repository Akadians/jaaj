using UnityEngine;

public interface ISkill
{
    void Skill();
}
public enum SkillType
{
    BULL_SKILL, CAT_SKILL, BAT_SKILL  
}

public class EnemyBehaviour : MonoBehaviour
{   
    public float lookDistance;
    public LayerMask raycastLayers;
    public Transform[] wayPoints;
    public float patrolSpeed;
    public bool isLookLeft;
    [HideInInspector]public int side;
    [HideInInspector]public Transform target;
    [HideInInspector]public int idWayPoint;

    private void Start()
    {
        target = wayPoints[0];    
    }

    public virtual void Patrol()
    {
        target.position = new Vector3(wayPoints[idWayPoint].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        
        WayPointControl();
        ControlFlip(target);
    }

    public void FlyPatrol(bool isRandom)
    {
        if(isRandom)
        {
            WayPointRandomControl();
        }
        else
        {
            WayPointControl();
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        
        ControlFlip(target);
    }

    void WayPointRandomControl()
    {
        if(transform.position == target.position)
        {
            idWayPoint = Random.Range(0, wayPoints.Length);
            target = wayPoints[idWayPoint];
        }
    }

    void WayPointControl()
    {
        if(transform.position == target.position)
        {
            idWayPoint++;
            if(idWayPoint >= wayPoints.Length)
            {
                idWayPoint = 0;
            }
            target = wayPoints[idWayPoint];
        }
    }

    public void ControlFlip(Transform targ)
    {
        if(transform.position.x > targ.position.x && isLookLeft)
        {
            Flip();
        }
        else if(transform.position.x < targ.position.x && !isLookLeft)
        {
            Flip();
        }
    }

    public virtual RaycastHit2D CheckRayCastHorizontal()
    {
        if(isLookLeft) { side = 1; } else { side = -1;}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * side, lookDistance, raycastLayers);
        Debug.DrawRay(transform.position, transform.right * side * lookDistance, Color.red, 0.2f);
        return hit;
    }

    public virtual RaycastHit2D CheckRayCastToPosition(Vector2 objectPosition)
    {
        Vector2 direction = ((Vector2)transform.position - objectPosition)*-1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, lookDistance, raycastLayers);
        Debug.DrawRay(transform.position, direction, Color.red, 0.2f);
        return hit;
    }

    public void Flip()
    {
        isLookLeft = !isLookLeft;
        
        float s = transform.localScale.x *-1;
        Vector3 newScale = new Vector3(s, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }

    public void Dead()
    {
        this.gameObject.layer = 16;
    }
}
