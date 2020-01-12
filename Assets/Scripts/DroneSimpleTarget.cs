using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneSimpleTarget : MonoBehaviour
{
    private NavMeshAgent agent;
    public float movementSpeed = 5;

    public Vector3 targetWaypoint;
    public Transform target;
    public bool tracking;

    public DroneSimpleLight droneLight;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tracking = false;
        targetWaypoint = transform.position;
        droneLight = transform.Find("DroneLight").GetComponent<DroneSimpleLight>();

        movementSpeed = 5;
    }

    void Update()
    {
        agent.SetDestination(targetWaypoint);

        if (tracking)
        {
            droneLight.tracking = true;
            droneLight.target = transform;
        }
        else
        {
            droneLight.tracking = false;
        }
    }
}
