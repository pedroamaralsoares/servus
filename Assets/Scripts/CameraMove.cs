using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public Transform target {get; set;}

    public float distance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = GameObject.Find("CameraTarget").transform;

        distance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f * distance);
        transform.LookAt(new Vector3 (target.position.x, player.position.y, target.position.z));
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -1f*distance), 3 * Time.deltaTime);
    }
}
