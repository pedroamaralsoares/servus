using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitNeon : MonoBehaviour
{
    private float posY;
    // Start is called before the first frame update
    void Start()
    {
        posY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0, Space.World);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, posY + Mathf.PingPong(Time.time, 2), transform.position.z) , 3 * Time.deltaTime);
    }
}
