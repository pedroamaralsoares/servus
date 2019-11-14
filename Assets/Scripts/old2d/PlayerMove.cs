using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PhysicsObject
{

    private Rigidbody rb;

    public float maxSpeedUp = 5;
    public float maxSpeedDown = 5;
    public float accelerationUp = 1.5f;
    public float accelerationDown = 1.5f;

    public float jumpTakeOffSpeed = 10;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 move;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        move = Vector2.zero;
    }

  

    protected override void ComputeVelocity()
    {
        //Vector2 move = Vector2.zero;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (move.x > 1)
                move.x = 1;
            else if (move.x < -1)
                move.x = -1;
            else
                move.x += accelerationUp * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
            //Debug.Log(accelerationUp * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        }
        else
        {
            if (move.x > 0)
            {
                move.x =  Input.GetAxis("Horizontal") / accelerationDown;
            }
            if (move.x < 0)
            {
                move.x = Input.GetAxis("Horizontal") / accelerationDown;
            }
            //move.x = Input.GetAxis("Horizontal");
            //Debug.Log("B");
        }
        //move.x = Input.GetAxisRaw("Horizontal") * accelerationUp /* * acceleration */;
       



        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        // cancelar o salto -> reduzir a força do salto:
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        /*
        if (Input.GetButtonDown("Jump") && nextToWall)
        {
            velocity.y = jumpTakeOffSpeed * 2f;

            if (!grounded) {
                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    move.x = -move.x;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    move.x = -move.x;
                }
            }

           
        }
        */
        


            if (move.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (move.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeedUp);


     

        targetVelocity = (move ) * maxSpeedUp;
    }
}
