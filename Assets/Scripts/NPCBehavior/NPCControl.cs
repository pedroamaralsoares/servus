using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCControl : MonoBehaviour
{
    public static int SeqID = 0;
    public int ID;
    public enum TransitionParameter
    {
        Move,
        Wonder,
    }

    public Animator Animator;

    public bool Move;
    public bool Wonder;
    public Transform Waypoint;

    public GameObject[] Drones;
    public MeshRenderer LED;

    private void Start()
    {
        Drones = GameObject.FindGameObjectsWithTag("Drone");
        LED = FindDeepChild(transform, "Sphere").GetComponent<MeshRenderer>();
        ID = SeqID++;
    }

    public GameObject FindClosestDrone()
    {
        GameObject closest = null;
        Vector3 pos = this.gameObject.transform.position;
        float distance = Mathf.Infinity;
        foreach (GameObject drone in Drones)
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
}
