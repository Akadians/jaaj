using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckCollision : MonoBehaviour
{
    private Player _Player;
    public int idPlayer;
    public ParticleSystem downParticle;

    private void Start()
    {
        _Player = FindObjectOfType(typeof(Player)) as Player;    
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.layer)
        {
            case 11:
                downParticle.Play();
            break;
        }

        switch(other.gameObject.tag)
        {
            case "EnemyHit":
                _Player.GetDamage(idPlayer);
            break;
        }
    }
}
