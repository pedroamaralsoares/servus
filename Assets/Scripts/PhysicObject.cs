using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    public float gravityModifierWall = 0.3f;

    protected Vector3 targetVelocity;
    protected bool grounded;
    protected float distToGround;
    protected Vector3 groundNormal;
    protected Rigidbody rb;
    protected Vector3 velocity;


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private BoxCollider col;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
       distToGround = col.bounds.extents.y - col.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector3.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        
        
        velocity += gravityModifier * Physics.gravity * Time.deltaTime;
    

        velocity.x = targetVelocity.x;

        grounded = false;
        transform.parent = null;


        Vector3 deltaPosition = velocity * Time.deltaTime;


        Debug.Log(groundNormal);

        Vector3 moveAlongGround = new Vector3(groundNormal.y, -groundNormal.x);

        Vector3 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector3.up * deltaPosition.y;
        Debug.Log("MOVE:" + move);

        Movement(move, true);

    }

    void Movement(Vector3 move, bool yMovement)
    {
        float distance = move.magnitude;
        float hitDistance = 0;

        if (yMovement) {
            bool groundCol = Physics.Raycast(transform.position, -Vector3.up,out RaycastHit hit, distToGround + 0.5f);
            if(groundCol) {
                groundNormal = hit.normal;
                hitDistance = hit.distance;
                grounded = true;
            }
            else {
                grounded = false;
            }
        }

        float projection = Vector3.Dot(velocity, groundNormal);
        if (projection < 0)
        {
            velocity = velocity - projection * groundNormal;
        }

        /*
        float modifiedDistance = hitDistance - shellRadius;
        distance = modifiedDistance < distance ? modifiedDistance : distance;
        */
        

    /*
        if (distance > minMoveDistance)
        {
            
            int count = rb.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            //if (count > 1) Debug.Log(count);


            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                Debug.DrawLine(transform.position, hitBuffer[i].transform.position, Color.red);
                hitBufferList.Add(hitBuffer[i]);
            }

            // review and use the distributed raycasts around the object
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                //Debug.Log(currentNormal);
                // check the angle for ground
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    transform.parent = hitBufferList[i].transform;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                //NEW
                // check the angle for ground
                if (!yMovement)
                {
                    if (currentNormal.x != 0)
                    {
                        nextToWall = true;

                    }
                    else
                    {
                        nextToWall = false;
                    }
                }
                /////



                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }
    */
        Debug.Log(grounded);
        rb.transform.position = rb.transform.position + move.normalized * distance;
    }
    
     protected bool IsGrounded() {
         Debug.DrawLine(transform.position, transform.position -Vector3.up, Color.red);
     return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.3f);
    }
}
