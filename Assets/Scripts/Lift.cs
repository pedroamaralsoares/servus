using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float oldHeight;
    public float speed;

    private bool back = false;

    public void Start()
    {
        oldHeight = transform.position.y;
    }

    public void Update()
    {
        if (back)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, oldHeight, transform.position.z), step);
        }

        if (Mathf.Approximately(transform.position.y, oldHeight))
            back = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Draggable")
        back = true;
    }
}
