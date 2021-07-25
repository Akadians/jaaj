using System;
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
        players[IdPlayer].rigB.velocity = movement * Time.deltaTime * Speed * 10;
    }

    void Skill()
    {
        if (Input.GetKeyDown(KeyCode.E) && IdPlayer == 0)
        {
            
            return;
        }
        else if (Input.GetKeyDown(KeyCode.E) && IdPlayer == 1)
        {
            
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (!IsJumping)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                Doublejump = true;
                return;
            }
            else if(Doublejump)
            {
                players[IdPlayer].rigB.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                Doublejump = false;
                return;
            }            
        }
    }
}



