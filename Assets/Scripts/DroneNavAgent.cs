using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneNavAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 leftWaypoint;
    public Vector3 rightWaypoint;

    public SceneryManager sceneryManager;
    public VirtualFloor virtualFloor;

    public Vector3 targetWaypoint;

    public DroneLight droneLight;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetWaypoint = transform.position;
        droneLight = transform.Find("DroneLight").GetComponent<DroneLight>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetWaypoint);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(targetWaypoint.x, transform.position.y, transform.position.z), 1 * Time.deltaTime);

        if (virtualFloor.tracking) {
            targetWaypoint = new Vector3(virtualFloor.latestPlayerPos.x, transform.position.y, virtualFloor.latestPlayerPos.z);

            if (Mathf.Abs(transform.position.x-targetWaypoint.x) < 5) {
                droneLight.tracking = true;
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
