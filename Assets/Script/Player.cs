using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

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

    [Header("Niu Shot")]
    public GameObject niuShotPrefab;
    public Transform niuGun;
    public int shotSpeed;
    public int shotCharges = 3;
    private int currentCharge; 
    public float addChargeTime = 2f;
    private bool isShoting;

    [Header("Config")]
    public int maxGodSend = 5;
    public int godsend;
    public int maxHp = 3;
    public float Speed;
    public float JumpForce;
    public LayerMask FloorCheck;
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
        currentCharge = shotCharges;
        _UIController.UpdateGodSendBar(godsend, maxGodSend);
    }

    void Update()
    {
        if (isDead) { movement = Vector3.zero; return; }
        Move();
        Changer();
        GroundCheck();
        Jump();        
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

    IEnumerator ResetCharge()
    {
        yield return new WaitForSeconds(addChargeTime);
        currentCharge ++;

        if(currentCharge >= shotCharges)
        {
            currentCharge = shotCharges;
            StopCoroutine(ResetCharge());
        }
        else
        {
            StartCoroutine(ResetCharge());
        }
    }

    IEnumerator DelayShot()
    {
        isShoting = true;
        SkillAnimation();
        yield return new WaitForSeconds(0.6f);
        GameObject temp = Instantiate(niuShotPrefab, niuGun.position, Quaternion.identity);
        temp.transform.right = niuGun.right;
        temp.GetComponent<Shot>().SetBulletSpeed(shotSpeed);
        isShoting = false;
    }

    void Skill()
    {
        if(currentCharge > 0)
        {
            StartCoroutine(DelayShot());
            StopCoroutine(ResetCharge());
            StartCoroutine(ResetCharge());
        }
    }

    void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.E) && IdPlayer == 0 && !isShoting)
        {
            Skill();
        }
        else if (Input.GetKeyDown(KeyCode.E) && players[IdPlayer].isCanInteract == true && players[IdPlayer].interactionObject != null)
        {
            SkillAnimation();
            switch (players[IdPlayer].interactionObject.interactionType)
            {
                case InteractionType.GET_SKILL:
                    players[IdPlayer].interactionObject.Interact();
                    break;

                case InteractionType.OBJECT_INTERACTION:
                    if (godsend >= players[IdPlayer].interactionObject.godSendRequired)
                    {
                        godsend -= players[IdPlayer].interactionObject.godSendRequired;
                        players[IdPlayer].interactionObject.Interact();
                        CheckInteractables();
                    }
                    else
                    {
                        _UIController.OpenAttentionPanel();
                    }
                    break;
            }
        }
    }

    public void GetGodSend(int qtd)
    {
        godsend += qtd;
        if (godsend >= maxGodSend)
        {
            godsend = maxGodSend;
        }
        _UIController.UpdateGodSendBar(godsend, maxGodSend);
    }

    public void ChangeSkill(SkillType newSkill)
    {
        currentSkill = newSkill;
        _UIController.ChangePowerIcon(currentSkill);
    }

    void SkillAnimation()
    {
        players[IdPlayer].anim.SetTrigger("Attack");
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
        _UIController.ChangeHUD(IdPlayer);
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
        if (players[IdPlayer].interactionObject != null)
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
            if (!players[IdPlayer].IsJumping)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                players[IdPlayer].jumpParticle.Play();
                //PlayerSound.PlayerJump();

                players[IdPlayer].IsJumping = true;
                players[IdPlayer].Doublejump = true;
                return;
            }
            else if (players[IdPlayer].Doublejump)
            {
                //zera a velocidade para manter a estabilidade do corpo
                players[IdPlayer].rigB.velocity = new Vector2(players[IdPlayer].rigB.velocity.x, 0);
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                players[IdPlayer].Doublejump = false;
                return;
            }
        }
    }

    void CameraFollow()
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

        if (!isDead)
        {
            players[id].rigB.gameObject.layer = 14;
        }
    }

    public void GetDamage(int id)
    {
        currentHp--;
        if (currentHp <= 0)
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
        Vector2 knockDir = (players[id].rigB.transform.right) * JumpForce;
        players[id].rigB.AddForce(new Vector2(knockDir.x, JumpForce), ForceMode2D.Impulse);
    }

    void Dead()
    {
        isDead = true;
        _UIController.OpenGameoverPanel();
    }

}



