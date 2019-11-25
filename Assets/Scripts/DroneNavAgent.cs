using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneNavAgent : MonoBehaviour
{

    private const int NPC_PRIORITY = 2;
    private const int PLAYER_PRIORITY = 1;
    public enum State
    {
        Sleeping,
        PlayerTargeting,
        NPCTargeting
    }
    private State state = State.Sleeping;

    public PriorityQueue<Transform, int> prios;

    public NavMeshAgent agent;
    public Vector3 leftWaypoint;
    public Vector3 rightWaypoint;

    public SceneryManager sceneryManager;
    private VirtualFloor virtualFloor;

    public Vector3 targetWaypoint;
    public Transform target;
    public bool tracking;
    public Vector3 initialPosition;


    public DroneLight droneLight;
    void Start()
    {
        prios = new PriorityQueue<Transform, int>(NPC_PRIORITY);
        agent = GetComponent<NavMeshAgent>();
        tracking = false;
        targetWaypoint = transform.position;
        initialPosition = transform.position;
        droneLight = transform.Find("DroneLight").GetComponent<DroneLight>();
    }

    void Update()
    {
        agent.SetDestination(targetWaypoint);

        if (tracking)
        {
            Transform check = prios.Top();

            if (check != null)
            {
                target = check;
                targetWaypoint = new Vector3(target.position.x, transform.position.y, target.position.z);
            }
            else
            {
                target = droneLight.gameObject.transform;
                targetWaypoint = new Vector3(initialPosition.x, 0, initialPosition.z);
            }

            if (Vector3.Distance(transform.position, targetWaypoint) < 10)
            {
                droneLight.tracking = true;
                droneLight.target = target;
                //if (prios.TopPriority() == NPC_PRIORITY)
                //{
                //    droneLight.trackingNPC = true;
                //}
                //else
                //{
                //    droneLight.trackingNPC = false;
                //}
            }
            else
            {
                droneLight.tracking = false;
            }
        }
        else
        {
            droneLight.tracking = false;
        }
    }
}
