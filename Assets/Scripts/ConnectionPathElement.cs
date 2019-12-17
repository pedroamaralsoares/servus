using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPathElement : MonoBehaviour
{
    public Vector3 target;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 6 * Time.deltaTime);
    }
}
