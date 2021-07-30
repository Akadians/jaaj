using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using FMODUnity;

[Serializable]
public struct PlayerStruct
{
    [Header("Player")]
    public SpriteRenderer sr;
    public Rigidbody2D rigB;
    public Animator anim;
    public Transform groundCheckA;
    public Transform groundCheckB;    

    [Header("FX")]
    public ParticleSystem jumpParticle;

    [Header("Bools")]
    public bool IsJumping;
    public bool Doublejump;

    [Header("Interaction System")]
    public Interaction interactionObject;
    public bool isCanInteract;
}


public class Player : MonoBehaviour
{
    private UIControler _UIController;

    public PlayerStruct[] players;

    [Header("Players Config")]
    public SkillType currentSkill;
    public SoundController PlayerSound;
    public int maxHp = 3;
    public float Speed;
    public float JumpForce;

    public LayerMask FloorCheck;

    public GameObject[] Power; // Lista com sprites dos projeteis.
    public CinemachineVirtualCamera CMCam;

    [Header("DamageConfig")]
    public Color damageColor1;
    public Color damageColor2;
    public float invencibilityTime1;
    public float invencibilityTime2;

    private bool isDead;
    private Vector3 movement;
    
    private int currentHp;
    private int IdPlayer;

    private void Start()
    {
        _UIController = FindObjectOfType(typeof(UIControler)) as UIControler;
        currentHp = maxHp;
    }

    void Update()
    {
        if(isDead) { movement = Vector3.zero; return;}
        Move();
        Changer();
        GroundCheck();
        Jump();
        Skill();
        Interaction();
    }

    void Move()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        players[IdPlayer].rigB.transform.position += movement * Time.deltaTime * Speed;

        if (movement != Vector3.zero)// Checkagem de movimento para animação.
        {
            players[IdPlayer].anim.SetBool("Move", true);            
        }
        else
        {
            players[IdPlayer].anim.SetBool("Move", false);            
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            players[IdPlayer].rigB.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetAxis("Horizontal") > 0f)
        {
            players[IdPlayer].rigB.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        players[0].anim.SetBool("Jumping", players[0].IsJumping);
        players[1].anim.SetBool("Jumping", players[1].IsJumping);
    }

    void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E) && players[IdPlayer].isCanInteract == true && players[IdPlayer].interactionObject != null)
        {
            players[IdPlayer].interactionObject.Interact();
        }
    }


    public void ChangeSkill(SkillType newSkill)
    {
        currentSkill = newSkill;
        _UIController.ChangePowerIcon(currentSkill);
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.E) && IdPlayer == 0)
        {
            //Instantiate(Power[0], transform.position, transform.rotation); ////Opção para Gerar projetil.
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E) && IdPlayer == 1)
        {
            //Instantiate(Power[1], transform.position, transform.rotation); //Opção para Gerar projetil.
            return;
        }

    }

    void Changer() //Metodo de troca do heroi.
    {
        
        if (Input.GetKeyDown(KeyCode.Q) && IdPlayer == 0)
        {
            ChangerSortingOrder(1);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && IdPlayer == 1)
        {
            ChangerSortingOrder(-1);
            return;
        }
        
        CameraFollow();
    }

    void ChangerSortingOrder(int nextId)
    {
        CheckInteractables();   
        players[IdPlayer].anim.SetBool("Move", false);
        players[IdPlayer].sr.sortingOrder = 0;
        IdPlayer += nextId;
        players[IdPlayer].sr.sortingOrder = 1;
        _UIController.ChangeHUD(IdPlayer);
    }

    void CheckInteractables()
    {
        if(players[IdPlayer].interactionObject != null)
        {
            players[IdPlayer].isCanInteract = false;
            players[IdPlayer].interactionObject.attentionIcon.SetActive(false);
            players[IdPlayer].interactionObject = null;
        }
    }

    void GroundCheck()
    {
        players[0].IsJumping = !Physics2D.OverlapArea(players[0].groundCheckA.position, players[0].groundCheckB.position, FloorCheck);
        players[1].IsJumping = !Physics2D.OverlapArea(players[1].groundCheckA.position, players[1].groundCheckB.position, FloorCheck);
    }


    void Jump()
    {       

        if (Input.GetButtonDown("Jump"))
        {
            if(!players[IdPlayer].IsJumping)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                players[IdPlayer].jumpParticle.Play();
                //PlayerSound.PlayerJump();

                players[IdPlayer].IsJumping = true;
                players[IdPlayer].Doublejump = true;                                
                return;
            }
            else if(players[IdPlayer].Doublejump)
            {
                //zera a velocidade para manter a estabilidade do corpo
                players[IdPlayer].rigB.velocity = new Vector2(players[IdPlayer].rigB.velocity.x, 0);
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                players[IdPlayer].Doublejump = false;                
                return;
            }            
        }
    }

    void CameraFollow ()
    {
        CMCam.Follow = players[IdPlayer].rigB.transform;
    }

    public IEnumerator InvencibleDelay(int id)
    {
        players[id].rigB.gameObject.layer = 17;
        players[id].sr.color = damageColor1;
        yield return new WaitForSeconds(invencibilityTime1);
        players[id].sr.color = damageColor2;
        yield return new WaitForSeconds(invencibilityTime2);
        players[id].sr.color = Color.white;

        if(!isDead)
        {
            players[id].rigB.gameObject.layer = 14;
        }
    }

    public void GetDamage(int id)
    {
        currentHp--;
        if(currentHp <= 0)
        {
            currentHp = 0;
            Dead();
        }

        StartCoroutine(InvencibleDelay(id));
        KnockBack(id);
        _UIController.UpdateHUD(currentHp);
    }

    void KnockBack(int id)
    {
        players[id].rigB.velocity = Vector2.zero;
        Vector2 knockDir = (players[id].rigB.transform.right)* JumpForce;
        players[id].rigB.AddForce(new Vector2(knockDir.x, JumpForce), ForceMode2D.Impulse);
    }
    
    void Dead()
    {
        isDead = true;
    }

}



