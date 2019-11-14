using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public Transform target {get; set;}
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
