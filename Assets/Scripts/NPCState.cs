using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour
{
    public enum State
    {
        Alert,
        Normal
    }

    public State state;

    private GameObject[] drones;

    private float jumpTimer = 2.0f;
    private Rigidbody rb;
    private float jumpForce = 100.0f;
    private float alertTimer = 5.0f;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        state = State.Normal;
        drones = GameObject.FindGameObjectsWithTag("Drone");
    }

    public void TriggerStateAlert()
    {
        if (state == State.Normal)
        {
            state = State.Alert;
            alertTimer = 5.0f;

            GameObject closestDrone = FindClosestDrone();
            closestDrone.GetComponent<DroneNavAgent>().tracking = true;
        }
    }

    private GameObject FindClosestDrone()
    {
        GameObject closest = null;
        Vector3 pos = this.gameObject.transform.position;
        float distance = Mathf.Infinity;
        foreach (GameObject drone in drones)
        {
            Vector3 diff = drone.transform.position - pos;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = drone;
                distance = curDistance;
            }
        }

        return closest;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Alert:

                alertTimer -= Time.deltaTime;
                if (alertTimer <= 0)
                {
                    state = State.Normal;
                    return;
                }

                jumpTimer -= Time.deltaTime;
                if (jumpTimer <= 0)
                {
                    rb.AddForce(0, jumpForce, 0);
                    jumpTimer = 1.0f;
                }

                break;

            case State.Normal:
                break;

            default:
                break;
        }
    }
}
