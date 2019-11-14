using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // have we been triggered?
    bool triggered;

    Transform gameCamera;

    Transform pointToLook; // where the camera has to focus on in the new area

    void Start () {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        pointToLook = transform.Find("Center");
    }
    void Awake()
    {
        triggered = false;
    }
    // called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter(Collider collider)
    {
        // check we haven't been triggered yet!
        if ( ! triggered)
        {
            // check we actually collided with 
            // a character. It would be best to
            // setup your layers so this check is
            // not required, by creating a layer 
            // "Checkpoint" that will only collide 
            // with characters.
            if (collider.gameObject.tag == "Player")
            {
                Trigger();
            }
        }
    }
    void Trigger()
    {
        // Tell the animation controller about our 
        // recent triggering
        triggered = true;
        Debug.Log("HI");
        gameCamera.GetComponent<CameraMove>().target = pointToLook;

    }
}
