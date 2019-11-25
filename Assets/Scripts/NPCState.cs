using System.Collections;
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
    private float jumpTimer = 1.0f;
    private float jumpForce = 100.0f;
    private float alertTimer = 3.0f;

    private Transform waypoint;
    private float wayRadius = 1.0f;
    private float speed = 5.0f;

    private void Start()
    {
        waypoint = GameObject.Find("Waypoint").transform;
        rb = this.gameObject.GetComponent<Rigidbody>();
        drones = GameObject.FindGameObjectsWithTag("Drone");
        led = this.transform.GetChild(0).GetComponent<MeshRenderer>();
        panel = GameObject.Find("ControlPanel").GetComponent<ControlPanel>();
        virtualFloor = GameObject.Find("Floor-wet").GetComponent<VirtualFloor>();
    }

    public void StartWalking()
    {
        state = State.Walking;

        // i have to trigger the drone, so he can check me

        GameObject closestDrone = FindClosestDrone();
        if (closestDrone != null)
        {
            closestDrone.GetComponent<DroneNavAgent>().tracking = true;
            closestDrone.GetComponent<DroneNavAgent>().prios.Insert(this.transform, 2);
        }  
    }

    public IEnumerator TriggerStateAlert()
    {
        yield return new WaitForSeconds (0.5f);
        if (state == State.Normal)
        {
            state = State.Alert;

            GameObject closestDrone = FindClosestDrone();
            if (closestDrone != null) {
                closestDrone.GetComponent<DroneNavAgent>().tracking = true;
                closestDrone.GetComponent<DroneNavAgent>().prios.Insert(GameObject.FindGameObjectWithTag("Player").transform, 1);
            }

            led.material.color = Color.yellow;
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
                        closestDrone.GetComponent<DroneNavAgent>().prios.Pop();
                    }

                    alertTimer = 7.0f;
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
                        closestDrone.GetComponent<DroneNavAgent>().prios.Pop();
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

        //Debug.Log(cover);
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
