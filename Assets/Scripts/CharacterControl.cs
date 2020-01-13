using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Grab,
        Pulling,
        Pushing,

        Dying,
    }

    public Animator animator;

    public float BlockDistance;

    public float moveIntensityMultiplier; // between 0 and 1
    public bool MoveRight;
    public bool MoveLeft;
    public bool Jump;
    public bool Grab;
    public bool Pushing;
    public bool Pulling;
    public bool Dying;

    public GameObject DraggableObject;
    public GameObject ColliderEdgePrefab;
    public List<GameObject> BottomSpheres = new List<GameObject>();
    public List<GameObject> FrontSpheres = new List<GameObject>();
    public List<Transform> Feet = new List<Transform>();
    public float GravityMultiplier;
    public float PullMultiplier;

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }

            return rigid;
        }
    }


    public Transform playerRig;
    public float playerRigPosZ = 0;

    private void Awake()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        float bottom = box.bounds.center.y - box.bounds.extents.y;
        float top = box.bounds.center.y + box.bounds.extents.y; ;
        float front = box.bounds.center.z + box.bounds.extents.z;
        float back = box.bounds.center.z - box.bounds.extents.z;

        GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));
        GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;
        topFront.transform.parent = this.transform;

        // adjust local position
        bottomFront.transform.localPosition = new Vector3 (0, bottomFront.transform.localPosition.y, bottomFront.transform.localPosition.z);
        bottomBack.transform.localPosition = new Vector3 (0, bottomBack.transform.localPosition.y, bottomBack.transform.localPosition.z);
        topFront.transform.localPosition = new Vector3 (0, topFront.transform.localPosition.y, topFront.transform.localPosition.z);

        BottomSpheres.Add(bottomFront);
        BottomSpheres.Add(bottomBack);

        FrontSpheres.Add(bottomFront);
        FrontSpheres.Add(topFront);

        float hSec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
        CreateMiddleSpheres(bottomFront, -this.transform.forward, hSec, 4, BottomSpheres);

        float vSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
        CreateMiddleSpheres(bottomFront, this.transform.up, vSec, 9, FrontSpheres);


        playerRig = transform.Find("player_mixamo_rig");

        // change the start position of the player
        GameObject[] checks = GameObject.FindGameObjectsWithTag("checkpoint_pos");
        if (checks.Length > 0)
            transform.position = new Vector3(checks[0].transform.position.x, transform.position.y, transform.position.z);

        // change the camera target & camera positions
        Transform cameraTarget = GameObject.Find("CameraTarget").transform;
        Transform camera = GameObject.Find("Main Camera").transform;

        cameraTarget.position = new Vector3(transform.position.x, cameraTarget.position.y, cameraTarget.position.z);
        camera.position = new Vector3(transform.position.x, camera.position.y, camera.position.z);

    }

    public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int iterations, List<GameObject> spheresList)
    {
        for (int i = 0; i < iterations; i++)
        {
            Vector3 pos = start.transform.position + (dir * sec * (i + 1));

            GameObject newObj = CreateEdgeSphere(pos);
            newObj.transform.parent = this.transform;

            // adjust local position
            newObj.transform.localPosition = new Vector3 (0, newObj.transform.localPosition.y, newObj.transform.localPosition.z);

            spheresList.Add(newObj);
        }
    }

    private void FixedUpdate()
    {
        if (RIGID_BODY.velocity.y < -0.2f)
            RIGID_BODY.velocity += (-Vector3.up * GravityMultiplier);

        if (RIGID_BODY.velocity.y > 0.2f && !Jump)
            RIGID_BODY.velocity += (-Vector3.up * PullMultiplier);

        playerRig.localPosition = Vector3.Lerp(playerRig.localPosition, new Vector3(0,-0.46f,playerRigPosZ), 6*Time.deltaTime);

        
        if (transform.position.y < 0) {
            LevelDebugManager levelDebugManager = GameObject.Find("LevelDebugManager").transform.GetComponent<LevelDebugManager>();
            levelDebugManager.Death();
        }
        

        if(Dying) {
            animator.SetBool(CharacterControl.TransitionParameter.Dying.ToString(), true);
            Dying = false;
        }
        
    }

    public GameObject CreateEdgeSphere(Vector3 pos)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
        return obj;
    }

    public bool CheckForDraggable()
    {
        foreach (GameObject o in FrontSpheres.GetRange(5, 5))
        {
            Debug.DrawRay(o.transform.position, transform.forward * BlockDistance);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, transform.forward, out hit, BlockDistance))
            {
                if (hit.collider.gameObject.tag == "Draggable")
                {
                    DraggableObject = hit.collider.gameObject;
                    return true;
                }
            }
        }

        return false;
    }
}
