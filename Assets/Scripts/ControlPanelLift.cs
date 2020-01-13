using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelLift : MonoBehaviour
{
    public Transform Lift;
    public GameObject[] npcs;

    public SceneryManager sceneryManager;
    public AudioClip sceneryClip;

    private AudioSource[] audioSources;

    public bool panelUsed;

    public bool playing;
    private float panelsTime;
    public Material runMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

    void Start()
    {
        panelsTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;
        playing = false;

        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (playing)
        {
            if (panelUsed) panelsTime += Time.deltaTime;

            meshRenderer.material = runMaterial;

            if (!audioSources[1].isPlaying)
            {
                Lift.gameObject.GetComponent<Lift>().canMove = true;
                foreach (GameObject npc in npcs)
                {
                    npc.GetComponentInChildren<Animator>().SetBool("Running", true);
                }
            }
        }

        else
        {
            meshRenderer.material = startMaterial;
            foreach (GameObject npc in npcs)
            {
                npc.GetComponentInChildren<Animator>().SetBool("Running", false);
            }
            Lift.gameObject.GetComponent<Lift>().canMove = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Trigger(collider);
    }
    void OnTriggerStay(Collider collider)
    {
        Trigger(collider);
    }

    void Trigger(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if ((Input.GetKeyDown("q") || Input.GetButtonDown("Fire1")) && sceneryManager != null)
            {
                if (!playing)
                {

                    // Play button press sound
                    audioSources[0].pitch = 3;
                    audioSources[0].volume = 1;
                    audioSources[0].Play();

                    audioSources[1].pitch = 1;
                    audioSources[1].volume = 0.2f;
                    audioSources[1].Play();

                    playing = true;
                    panelUsed = true;
                }
            }

        }
    }
}
