using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool opened = false;

    public Vector3 changePos;
    public Vector3 defaultPos;
    void Start()
    {
        defaultPos = transform.position;
        changePos = new Vector3(defaultPos.x + changePos.x, defaultPos.y + changePos.y, defaultPos.z + changePos.z);
    }


    void Update()
    {
        if (opened) {
            transform.position = Vector3.Lerp(transform.position, changePos, 6 * Time.deltaTime);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, defaultPos, 6 * Time.deltaTime);
        }
    }
}
