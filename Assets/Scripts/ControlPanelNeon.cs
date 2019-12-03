﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelNeon : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public SceneryManager sceneryManager;
    public AudioClip sceneryClip;

    private AudioSource audioSource;
    public float timeLimit = 3f;

    public bool panelUsed;

    public bool playing;
    private float panelsTime;
    public Material runMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

    public ControlPanelNeon[] panels;

    public NeonCube[] neonCubes;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        panelsTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;
        playing = false;

        audioSource = GetComponent<AudioSource>();
    }

    void Update () {
        if (playing) {
            if (panelUsed) panelsTime += Time.deltaTime;

            meshRenderer.material = runMaterial;

            if (panelsTime > timeLimit)
            {
                playing = false;

                // Play button press sound
                audioSource.pitch = 1f;
                audioSource.volume = 0.8f; 
                audioSource.Play();
                

                foreach (NeonCube nc in neonCubes) {
                    nc.activated = true;
                    nc.SwitchMaterial();
                }

                panelsTime = 0;
            }
        }

        else {
            meshRenderer.material = startMaterial;
        }
    }

    void OnTriggerEnter (Collider collider) {
        Trigger(collider);
    }
    void OnTriggerStay(Collider collider)
    {
        Trigger(collider);
    }

    void Trigger (Collider collider) {
         if (collider.gameObject.tag == "Player")
        {
            if ((Input.GetKeyDown("q") || Input.GetButtonDown("Fire1")) && sceneryManager != null) {
                if (!playing && panelsTime <= timeLimit) {

                    // Play button press sound
                    audioSource.pitch = 3;
                    audioSource.volume = 1;
                    GetComponent<AudioSource>().Play();

                    foreach (ControlPanelNeon cp in panels)
                    {
                        cp.playing = true;
                        cp.panelUsed = true;
                    }

                    foreach (NeonCube nc in neonCubes) {
                        nc.activated = false;
                        nc.SwitchMaterial();
                    }
                }
                
            }
        }
    }
}
