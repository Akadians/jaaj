using UnityEngine;

public interface ISkill
{
    void Skill();
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
    
    public virtual void Patrol()
    {
        target.position = new Vector3(wayPoints[idWayPoint].position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
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

    public virtual RaycastHit2D CheckRayCast()
    {
        if(isLookLeft) { side = 1; } else { side = -1;}
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * side, lookDistance, raycastLayers);
        Debug.DrawRay(transform.position, transform.right * side * lookDistance, Color.red, 0.2f);
        return hit;
    }

    public virtual void Flip()
    {
        isLookLeft = !isLookLeft;
        
        float s = transform.localScale.x *-1;
        Vector3 newScale = new Vector3(s, transform.localScale.y, transform.localScale.z);
        transform.localScale = newScale;
    }
}
