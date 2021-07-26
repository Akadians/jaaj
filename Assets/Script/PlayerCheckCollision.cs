using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    public ParticleSystem downParticle;

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.layer)
        {
            case 11:
                downParticle.Play();
            break;
        }
    }
}
