using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerStruct
{
    [Header("Player")]
    public Rigidbody2D rigB;
    public Animator anim;
    public Transform groundCheckA;
    public Transform groundCheckB;

    [Header("FX")]
    public ParticleSystem jumpParticle;

    [Header("Bools")]
    public bool IsJumping;
    public bool Doublejump;
}


public class Player : MonoBehaviour
{
    public PlayerStruct[] players;

    public float Speed;
    public float JumpForce;

    public int IdPlayer;

    public LayerMask FloorCheck;

    public GameObject[] Power; // Lista com sprites dos projeteis.

    private Vector3 movement;

    
    void Update()
    {
        Move();
        Changer();
        GroundCheck();
        Jump();
        Skill();       
    }

    void Move()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        players[IdPlayer].rigB.transform.position += movement * Time.deltaTime * Speed;

        if (movement != Vector3.zero)// Checkagem de movimento para animação.
        {
            //players[IdPlayer].anim.SetBool("Move", true);            
        }
        else
        {
            //players[IdPlayer].anim.SetBool("Move", false);            
        }
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
            IdPlayer++;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && IdPlayer == 1)
        {
            IdPlayer--;
            return;
        }
    }

    void GroundCheck()
    {
        players[IdPlayer].IsJumping = !Physics2D.OverlapArea(players[IdPlayer].groundCheckA.position, players[IdPlayer].groundCheckB.position, FloorCheck);
    }


    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(!players[IdPlayer].IsJumping)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                players[IdPlayer].jumpParticle.Play();

                players[IdPlayer].IsJumping = true;
                players[IdPlayer].Doublejump = true;
                //players[IdPlayer].anim.SetBool("Jump", true); // Trigger da animação pulo.
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
}



