﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DroneLight : MonoBehaviour
{
    Transform player;
    public bool tracking;

    private DroneControl droneControl;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        droneControl = transform.parent.GetComponent<DroneControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking && player != null) {
            SmoothLookAt(player);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Debug.DrawRay(transform.position,fwd*15, Color.green,2);
        if (Physics.Raycast(transform.position,fwd, out hit, 200))
            Debug.Log(hit.collider.name);
            if (hit.collider != null && hit.collider.tag == "Player") {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("DIE");
            }
                
    }


    void SmoothLookAt (Transform target) {
        Vector3 lTargetDir = target.position - transform.position;
         transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 1.5f);
    }
}
