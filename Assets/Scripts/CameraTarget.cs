using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public Transform target {get; set;}

    public Vector3 offsetFromFocusPoint = new Vector3(0,0,0); // mostly used when focus point is the player

    public bool fastX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player;
        //transform.parent = target;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = Vector3.Lerp(transform.localPosition, offsetFromFocusPoint, 1 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.localPosition, target.position + offsetFromFocusPoint, 2 * Time.deltaTime);

        if (fastX) {
            transform.position = Vector3.Lerp(transform.localPosition, new Vector3(target.position.x + offsetFromFocusPoint.x,transform.position.y,transform.position.z), 4 * Time.deltaTime);
        }
        
    }
}
