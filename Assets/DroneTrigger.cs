using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTrigger : MonoBehaviour
{
    DroneSimpleTarget drone;
    public Transform waypoint;
    void Start()
    {
        drone = GameObject.FindGameObjectWithTag("Drone").GetComponent<DroneSimpleTarget>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            drone.tracking = true;
            drone.targetWaypoint = waypoint.position;
        }
    }
}
