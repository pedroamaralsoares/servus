using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneNavAgent : MonoBehaviour
{
    public IDictionary<string, Transform> npcTargets;
    public int playerTarget;

    private NavMeshAgent agent;
    public float movementSpeed = 5;
    public Vector3 leftWaypoint;
    public Vector3 rightWaypoint;

    public Vector3 targetWaypoint;
    public Transform target;
    public bool tracking;
    public Vector3 initialPosition;

    private GameObject player;

    public DroneLight droneLight;
    void Start()
    {
        npcTargets = new Dictionary<string, Transform>();
        agent = GetComponent<NavMeshAgent>();
        tracking = false;
        playerTarget = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        targetWaypoint = transform.position;
        initialPosition = transform.position;
        droneLight = transform.Find("DroneLight").GetComponent<DroneLight>();

        movementSpeed = 5;
    }

    void Update()
    {
        agent.SetDestination(targetWaypoint);

        if (tracking)
        {
            if (playerTarget != 0 && player.activeSelf)
            {
                target = player.transform;
                targetWaypoint = new Vector3(target.position.x, transform.position.y, target.position.z);
            }
            else if (npcTargets.Count != 0)
            {
                var e = npcTargets.GetEnumerator();
                e.MoveNext();
                target = e.Current.Value;
                targetWaypoint = new Vector3(target.position.x, transform.position.y, target.position.z);
            }
            else
            {
                target = droneLight.gameObject.transform;
                targetWaypoint = new Vector3(initialPosition.x, 0, initialPosition.z);
                tracking = false;
            }

            if (Vector3.Distance(transform.position, targetWaypoint) < 8)
            {
                droneLight.tracking = true;
                droneLight.target = target;
                agent.speed = movementSpeed * 0.8f;
            }
            else if (Vector3.Distance(transform.position, targetWaypoint) < 16)
            {
                droneLight.tracking = false;
                agent.speed = movementSpeed * 2.5f;
            }
            else if (Vector3.Distance(transform.position, targetWaypoint) < 30)
            {
                droneLight.tracking = false;
                agent.speed = movementSpeed * 4.5f;
            }
            else
            {
                droneLight.tracking = false;
                agent.speed = movementSpeed * 6f;
            }
        }
        else
        {
            droneLight.tracking = false;
        }
    }
}
