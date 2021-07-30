using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Interaction : MonoBehaviour
{
    private Player _Player;

    public enum InteractionType
    {
        GET_SKILL, OBJECT_INTERACTION
    }

    public InteractionType interactionType;
    public SkillType skillType;
    public GameObject attentionIcon;

    private void Start()
    {
        _Player = FindObjectOfType(typeof(Player)) as Player;    
    }

    public void Interact()
    {
        switch(interactionType)
        {
            case InteractionType.GET_SKILL:
                _Player.ChangeSkill(skillType);
            break;

            case InteractionType.OBJECT_INTERACTION:
                this.gameObject.SendMessage("Interaction", SendMessageOptions.RequireReceiver);
            break;
        }
    }
}
