using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private float speed;
    private bool isWithSpeed;
    void Update()
    {
        if(isWithSpeed)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    public void SetBulletSpeed(float s)
    {
        speed = s;
        isWithSpeed = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Player":

            break;

            default:
                Destroy(this.gameObject);
            break;
        }
    }
}
