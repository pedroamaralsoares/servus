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

    public bool tracking;

    public DroneLight droneLight;
    void Start()
    {
        tracking = false;
        targetWaypoint = leftWaypoint;
        droneLight = transform.Find("DroneLight").GetComponent<DroneLight>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetWaypoint.x, transform.position.y, transform.position.z), 1 * Time.deltaTime);

        if (tracking) {
            targetWaypoint = virtualFloor.latestPlayerPos;

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
