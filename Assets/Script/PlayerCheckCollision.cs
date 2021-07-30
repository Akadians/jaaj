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

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.layer)
        {
            case 18:
                if(idPlayer == 1)
                {
                    if(_Player.players[idPlayer].interactionObject == null)
                    {
                     _Player.players[idPlayer].interactionObject = other.GetComponent<Interaction>();
                    }
                    if(_Player.players[idPlayer].interactionObject.isSecondInteraction &&  _Player.players[idPlayer].interactionObject.interactionType == InteractionType.OBJECT_INTERACTION)
                    {return;}
                    _Player.players[idPlayer].isCanInteract = true;
                    _Player.players[idPlayer].interactionObject.attentionIcon.SetActive(true);
                }
            break; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch(other.gameObject.layer)
        {
            case 18:
                _Player.players[idPlayer].isCanInteract = false;
                if(_Player.players[idPlayer].interactionObject != null)
                {
                    _Player.players[idPlayer].interactionObject.attentionIcon.SetActive(false);
                    _Player.players[idPlayer].interactionObject = null;
                }
            break; 
        }
    }
}
