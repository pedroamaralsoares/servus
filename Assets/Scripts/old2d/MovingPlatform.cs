using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    //public Transform wa

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(Mathf.Sin(Time.time), -2.0f, 2.0f);
        transform.position = pos;
        //transform.position = Vector2.Lerp(transform.position,waypoint)
    }
}
