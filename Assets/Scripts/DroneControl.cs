using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 leftWaypoint;
    public Vector3 rightWaypoint;

    public SceneryManager sceneryManager;
    public VirtualFloor virtualFloor;

    public Vector3 targetWaypoint;
    void Start()
    {
        targetWaypoint = leftWaypoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetWaypoint.x, transform.position.y, transform.position.z), 1 * Time.deltaTime);

        targetWaypoint = virtualFloor.latestPlayerPos;
    }
}
