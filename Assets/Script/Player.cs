using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerStruct
{
    public Rigidbody2D rigB;
    public Animator anim;
}


public class Player : MonoBehaviour
{
    public PlayerStruct[] players;

    public float Speed;
    public float JumpForce;

    public int IdPlayer;

    public bool IsJumping;
    public bool Doublejump;

    public LayerMask FloorCheck;

    public GameObject[] Power; // Lista com sprites dos projeteis.

    private Vector3 movement;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Changer();
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

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!IsJumping)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                IsJumping = true;
                Doublejump = true;
                //players[IdPlayer].anim.SetBool("Jump", true); // Trigger da animação pulo.
                return;
            }
            else if (Doublejump)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                Doublejump = false;                
                return;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // Não Funcional.
    {        
        if (collision.gameObject.layer == FloorCheck)
        {
            IsJumping = false;
            Debug.Log("Check");
            return;
            //players[IdPlayer].anim.SetBool("Jump", false);
        }
    }
}



