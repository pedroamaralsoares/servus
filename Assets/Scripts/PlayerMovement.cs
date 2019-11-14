using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicObject {

	private Vector3 move;

    public float maxSpeedUp = 5;
    public float maxSpeedDown = 5;
    public float accelerationUp = 1.5f;
    public float accelerationDown = 1.5f;

    public float jumpTakeOffSpeed = 10;


	void Awake () {
		rb = this.GetComponent<Rigidbody>();
	}
	
	void Start () {
		move = Vector3.zero;
	}


    protected override void ComputeVelocity() {
		 if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (move.x > 1)
                move.x = 1;
            else if (move.x < -1)
                move.x = -1;
            else
                move.x += accelerationUp * Input.GetAxisRaw("Horizontal") * Time.deltaTime;

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


        }
        //Debug.Log(grounded);

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

		targetVelocity = (move ) * maxSpeedUp;

	}
    
}
