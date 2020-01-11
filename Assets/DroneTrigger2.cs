using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTrigger2 : MonoBehaviour
{
    DroneSimpleLight2 droneLight;
    void Start()
    {
        droneLight = GameObject.FindGameObjectWithTag("Drone").transform.Find("DroneLight").GetComponent<DroneSimpleLight2>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            droneLight.tracking = true;
            GameObject.FindGameObjectWithTag("Drone").transform.GetComponent<Animator>().SetBool("moving",true);
        }
    }
}
