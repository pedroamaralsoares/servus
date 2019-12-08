using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public Transform target {get; set;}

    public float distance;

    float smoothTime = 0.3f;
    float yVelocity = 0.0f;
    float newDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("CameraTarget").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, -1f * distance);
        transform.LookAt(new Vector3 (target.position.x, (player.position.y + target.position.y)/2, target.position.z));

        newDistance = Mathf.SmoothDamp(newDistance, -1f * distance, ref yVelocity, smoothTime);
        Debug.Log(newDistance);
        //float newDistance = Mathf.Clamp(transform.position.z + 1 * Time.deltaTime, minMyValue, maxMyValue);

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, newDistance), 3 * Time.deltaTime);
            
    
    }
}
