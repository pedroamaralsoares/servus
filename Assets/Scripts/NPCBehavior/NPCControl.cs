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
        Float,
    }

    public Animator Animator;

    public bool Move;
    public bool Wonder;
    public bool Rain;
    public bool Float;
    public Transform Waypoint;

    public GameObject[] Drones;
    public GameObject ColliderEdgePrefab;
    public List<GameObject> MidSpheres = new List<GameObject>();
    public MeshRenderer LED;

    public GameObject UmbrellaPrefab;
    private GameObject UmbrellaInstance;

    private void Start()
    {
        Drones = GameObject.FindGameObjectsWithTag("Drone");
        LED = FindDeepChild(transform, "Sphere").GetComponent<MeshRenderer>();
        ID = SeqID++;

        BoxCollider box = GetComponent<BoxCollider>();
        float yCenter = box.bounds.center.y;
        float xLeft = box.bounds.center.x + box.bounds.extents.z;
        float xRight = box.bounds.center.x - box.bounds.extents.z;
        float front = box.bounds.center.z - box.bounds.extents.z;

        GameObject centerLeft = Instantiate(ColliderEdgePrefab, new Vector3(xLeft, yCenter, front), Quaternion.identity);
        GameObject centerRight = Instantiate(ColliderEdgePrefab, new Vector3(xRight, yCenter, front), Quaternion.identity);

        centerLeft.transform.parent = this.transform;
        centerRight.transform.parent = this.transform;
        //centerLeft.transform.localPosition = new Vector3(centerLeft.transform.localPosition.x, centerLeft.transform.localPosition.y, 0);
        //centerRight.transform.localPosition = new Vector3(centerRight.transform.localPosition.x, centerRight.transform.localPosition.y, 0);
        MidSpheres.Add(centerLeft);
        MidSpheres.Add(centerRight);

        float hSec = (centerRight.transform.position - centerLeft.transform.position).magnitude / 5;
        CreateMiddleSpheres(centerLeft, this.transform.right, hSec, 4, MidSpheres);
    }

    private void Update()
    {
        if (VirtualInputManager.Instance.Rain)
        {
            if (!Rain)
            {
                UmbrellaInstance = Instantiate(UmbrellaPrefab, this.transform);
                Rain = true;
            }
        }
        else
        {
            if (Rain)
            {
                Destroy(UmbrellaInstance);
                Rain = false;
            }
        }
    }

    public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec, int iterations, List<GameObject> spheresList)
    {
        for (int i = 0; i < iterations; i++)
        {
            Vector3 pos = start.transform.position + (dir * sec * (i + 1));

            GameObject newObj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            newObj.transform.parent = this.transform;

            // adjust local position
            //newObj.transform.localPosition = new Vector3(newObj.transform.localPosition.x, newObj.transform.localPosition.y, 0);

            spheresList.Add(newObj);
        }
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

    public bool CheckForDraggable()
    {
        int angle = 45;
        int startAngle = (int)(-angle * 0.5);
        int increment = (int)(angle / MidSpheres.Count);

        Vector3 targetPos = Vector3.zero;

        float distance = 15f;

        RaycastHit hit;
        foreach (GameObject s in MidSpheres)
        {
            startAngle += increment;
            targetPos = (Quaternion.Euler(0, startAngle, 0) * transform.forward).normalized;
            Debug.DrawRay(s.transform.position, targetPos * distance);

            if (Physics.Raycast(s.transform.position, targetPos, out hit, distance))
            {
                if (hit.collider.gameObject.tag == "Draggable")
                {
                    if (hit.collider.transform.GetComponent<NeonCube>().domesticatedTouched) {
                        Debug.Log("yeet");
                        return true;
                    }
                    
                }
            }
        }

        return false;
    }
}
