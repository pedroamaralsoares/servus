﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // have we been triggered?
    bool triggered;

    public Transform gameCamera;
    public Transform cameraTarget;

    public bool FarToCenter;
    Transform pointToLook; // where the camera has to focus on in the new area
    public Vector3 newOffsetFromFocusPoint = new Vector3(15,0,10);
    public float newDistance;

    public bool fastX;


    /* Restart checkpoint - save position & objects */
    public bool isCheckpointPosition;
    public Vector3 inRestartPlayerPositionOffset;
    public Transform inRestartNewPrefab; // object important for future progression (draggable, for example)
    public Vector3 inRestartNewPrefabPositionOffset;

    /* -------------- */

    private float elapsed = 0.0f;

    void Start () {
        gameCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        cameraTarget = GameObject.Find("CameraTarget").transform;
        if (FarToCenter) pointToLook = transform.Find("Center");
        else pointToLook = GameObject.FindGameObjectWithTag("Player").transform;
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
    public void Trigger()
    {
        // Tell the animation controller about our 
        // recent triggering
        //triggered = true;

        cameraTarget.GetComponent<CameraTarget>().offsetFromFocusPoint = newOffsetFromFocusPoint;

        if (newDistance > 0) {
                
            gameCamera.GetComponent<CameraMove>().distance = newDistance;
        }

        cameraTarget.GetComponent<CameraTarget>().fastX = fastX;
        gameCamera.GetComponent<CameraMove>().isRotY = fastX;

        if (isCheckpointPosition) {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");
            objs[0].transform.position = new Vector3(transform.position.x + inRestartPlayerPositionOffset.x, objs[0].transform.position.y, objs[0].transform.position.z);

            if (GameObject.Find("LevelDebugManager")) {
                LevelDebugManager levelDebugManager = GameObject.Find("LevelDebugManager").transform.GetComponent<LevelDebugManager>();
                levelDebugManager.inRestartNewPrefab = inRestartNewPrefab;
                levelDebugManager.inRestartNewPrefabPosition = transform.position + inRestartNewPrefabPositionOffset;
                levelDebugManager.lastCheckpoint = this;
            }
        }
    }

}
