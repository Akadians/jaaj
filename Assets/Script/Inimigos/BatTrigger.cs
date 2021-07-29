using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatTrigger : MonoBehaviour
{
    [SerializeField]private Bat bat;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(bat.currentState == BatState.DEAD) {return;}
        if(other.gameObject.tag == "Player")
        {
            RaycastHit2D hit = bat.behaviour.CheckRayCastToPosition(other.transform.position);

            if(hit.collider != null)
            {
                print(hit.collider.gameObject.name);
                if(hit.collider.gameObject.tag == "Player")
                {
                    bat.playerTransform = other.transform;
                    bat.isDetectedPlayer = true;
                }
                else
                {
                    bat.ResetPlayerDetection();
                }
            }
            else
            {
                bat.ResetPlayerDetection();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(bat.currentState == BatState.DEAD) {return;}
        if(other.gameObject.tag == "Player")
        {
            bat.ResetPlayerDetection();
        }
    }
}
