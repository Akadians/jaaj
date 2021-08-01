using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType
{
    GET_SKILL, OBJECT_INTERACTION
}

[RequireComponent(typeof(BoxCollider2D))]
public class Interaction : MonoBehaviour
{
    private Player _Player;

    public InteractionType interactionType;
    public SkillType skillType;
    public GameObject attentionIcon;
    public int godSendRequired = 1;
    public int qtdGodSend = 1;
    public bool isSecondInteraction;

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
                if(!isSecondInteraction)
                {
                    _Player.GetGodSend(qtdGodSend);
                    _Player.AddLife();
                }

            break;

            case InteractionType.OBJECT_INTERACTION:
                if(!isSecondInteraction)
                {
                    this.gameObject.SendMessage("Interaction", SendMessageOptions.RequireReceiver);
                }
            break;
        }
        isSecondInteraction = true;
    }
}
