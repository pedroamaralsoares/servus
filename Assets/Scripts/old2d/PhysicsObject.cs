using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;
    public float gravityModifierWall = 0.3f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected bool nextToWall;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);


    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        
        // super meat boy style -- ganhar altitude colado à parede
        
        if (nextToWall)
        {
            velocity += gravityModifierWall * Physics2D.gravity * Time.deltaTime;
        }
        else
        {
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        }



        //velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;


        velocity.x = targetVelocity.x;

        grounded = false;
        nextToWall = false;
        transform.parent = null;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);

        if (nextToWall) Debug.Log("WALL");
        else Debug.Log("---");
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
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

        rb2d.transform.position = (Vector2) rb2d.transform.position + move.normalized * distance;
    }
}
