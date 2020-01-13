using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float oldHeight;
    public float newHeight;
    public float speed;

    private bool move = false;
    private bool back = false;
    public bool canMove = false;

    public void Start()
    {
        oldHeight = transform.position.y;
    }

    public void Update()
    {
        if (move)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, newHeight, transform.position.z), step);
        }
        if (back)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, oldHeight, transform.position.z), step);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (canMove)
        {
            if (collision.gameObject.tag == "Draggable")
            {
                StartCoroutine(OnTrigger(collision));
            }
        }
    }

    private IEnumerator OnTrigger(Collision collision)
    {
        yield return new WaitForSeconds(2f);
        move = true;
        back = false;
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (canMove)
        {
            if (collision.gameObject.tag == "Draggable")
            {
                StartCoroutine(OffTrigger(collision));
            }
        }
    }

    private IEnumerator OffTrigger(Collision collision)
    {
        yield return new WaitForSeconds(2f);
        move = false;
        back = true;
        collision.gameObject.transform.parent = null;
    }
}
