using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour
{

    public enum State
    {
        Sleeping,
        PlayerTargeting,
        NPCTargeting
    }
    private State state = State.Sleeping;

    public PriorityQueue<Transform, int> prios;

    public Vector3 leftWaypoint;
    public Vector3 rightWaypoint;

    public SceneryManager sceneryManager;
    public VirtualFloor virtualFloor;

    public Transform target;
    public Vector3 targetWaypoint;

    public List <Transform> uncheckedNPCs;

    public bool tracking;

    public DroneLight droneLight;
    void Start()
    {
        tracking = false;
        targetWaypoint = leftWaypoint;
        droneLight = transform.Find("DroneLight").GetComponent<DroneLight>();
        prios = new PriorityQueue<Transform, int>(0);
    }

    public IEnumerator CheckNPC()
    {
        // play animation of light
        yield return new WaitForSeconds (1f);
        // check!
        prios.Pop(); 
    }

    void Update()
    {
        if (uncheckedNPCs.Count > 0) {
            state = State.NPCTargeting;
            tracking = true;
        }
        else {
            state = State.PlayerTargeting;
        }
        
        targetWaypoint = target.position;

        transform.position = Vector3.Lerp(transform.position, new Vector3(targetWaypoint.x, transform.position.y, transform.position.z), 1 * Time.deltaTime);

        if (tracking) {
            targetWaypoint = virtualFloor.latestPlayerPos;

            if (state == State.PlayerTargeting && Mathf.Abs(transform.position.x-targetWaypoint.x) < 5)
                if (state == State.NPCTargeting && Vector3.Distance(transform.position, targetWaypoint) < 5) {
                
                    if (Mathf.Abs(transform.position.x-targetWaypoint.x) < 5) {
                        droneLight.tracking = true;
                        CheckNPC();
                    }
                }
            
            else {
                droneLight.tracking = false;
            }
        }
        else {
            droneLight.tracking = false;
        }


    }
}
