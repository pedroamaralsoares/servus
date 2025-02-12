﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCState : MonoBehaviour
{
    public enum State
    {
        Alert,
        Normal,
        Walking,
        Raining,
        Clear
    }

    private State state = State.Normal;
    private State cover = State.Clear;

    private GameObject[] drones;
    public GameObject umbrellaPrefab;
    public VirtualFloor virtualFloor;
    private GameObject umbrellaInstance;
    private MeshRenderer led;
    private ControlPanel panel;

    private Rigidbody rb;
    private float jumpTimer = 0f;
    private float jumpForce = 100.0f;
    private float alertTimer = 5.0f;

    private Transform waypoint;
    private float wayRadius = 1.0f;
    private float speed = 5.0f;

    public static int Count = 0;
    private int myCount;

    public Transform questionMark;

    private void Start()
    {
        waypoint = GameObject.Find("Waypoint").transform;
        rb = this.gameObject.GetComponent<Rigidbody>();
        drones = GameObject.FindGameObjectsWithTag("Drone");
        Debug.Log(this.transform.GetChild(0).transform.childCount);
        led = FindDeepChild(this.transform, "Sphere").GetComponent<MeshRenderer>();
        panel = GameObject.Find("ControlPanel").GetComponent<ControlPanel>();
        virtualFloor = GameObject.Find("Floor-wet").GetComponent<VirtualFloor>();
        myCount = Count++;
    }

    public Transform FindDeepChild(Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    public void StartWalking()
    {
        state = State.Walking;

        // i have to trigger the drone, so he can check me

        GameObject closestDrone = FindClosestDrone();
        if (closestDrone != null)
        {
            closestDrone.GetComponent<DroneNavAgent>().tracking = true;
            closestDrone.GetComponent<DroneNavAgent>().npcTargets.Add("npc" + myCount, this.transform);
        }  
    }

    public IEnumerator TriggerStateAlert()
    {
        Instantiate(questionMark, new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z), questionMark.rotation);
        yield return new WaitForSeconds (0.5f);
        if (state == State.Normal)
        {
            state = State.Alert;

            GameObject closestDrone = FindClosestDrone();
            if (closestDrone != null && GameObject.FindGameObjectWithTag("Player") != null) {
                closestDrone.GetComponent<DroneNavAgent>().tracking = true;
                closestDrone.GetComponent<DroneNavAgent>().playerTarget++;
            }

            led.material.color = Color.yellow;
            led.material.SetColor("_EmissionColor", Color.yellow);
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
                    led.material.color = Color.green;
                    GameObject closestDrone = FindClosestDrone();

                    if (closestDrone != null)
                    {
                        closestDrone.GetComponent<DroneNavAgent>().playerTarget--;
                    }

                    alertTimer = 10.0f;
                    return;
                }

                jumpTimer -= Time.deltaTime;
                if (jumpTimer <= 0)
                {
                    rb.AddForce(0, jumpForce, 0);
                    jumpTimer = 1.0f;
                }

                break;

            case State.Walking:
                if (Vector3.Distance(waypoint.position, this.transform.position) < wayRadius)
                {
                    GameObject closestDrone = FindClosestDrone();
                    if (closestDrone != null)
                    {
                        closestDrone.GetComponent<DroneNavAgent>().npcTargets.Remove("npc" + myCount);
                    }
                    Destroy(this.gameObject);
                }
                this.transform.position = Vector3.MoveTowards(this.transform.position, this.waypoint.position, Time.deltaTime * this.speed);
                break;

            case State.Normal:
                break;

            default:
                break;
        }

        if (cover == State.Clear && panel.playing)
        {
            umbrellaInstance = Instantiate(umbrellaPrefab, this.transform);
            cover = State.Raining;

            if (state == State.Walking)
            {
                speed += 3;
            }
        }
;
        if (cover == State.Raining && !panel.playing)
        {
            cover = State.Clear;
            Destroy(umbrellaInstance);

            if (state == State.Walking)
            {
                speed -= 3;
            }
        }
    }
}
