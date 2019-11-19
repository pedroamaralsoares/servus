using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Transform WaypointGO;
    public float Speed;
    private float WRadius = 1;
    public bool Walking = false;

    private void Start()
    {
        WaypointGO = GameObject.Find("Waypoint").transform;
    }

    private void Update()
    {
        if (this.Walking)
        {
            if (Vector3.Distance(WaypointGO.position, this.transform.position) < this.WRadius)
            {
                Destroy(this.gameObject);
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.WaypointGO.position, Time.deltaTime * this.Speed);
        }
    }
}

