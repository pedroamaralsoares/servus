using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelLift : MonoBehaviour
{

    public Transform Lift;
    public GameObject[] npcs;

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

    void Start()
    {
        panelsTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;
        playing = false;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playing)
        {
            if (panelUsed) panelsTime += Time.deltaTime;

            meshRenderer.material = runMaterial;

            if (panelsTime > timeLimit)
            {
                playing = false;
                panelUsed = false;
                panelsTime = 0;
            }

            Lift.gameObject.GetComponent<Lift>().canMove = true;
            foreach (GameObject npc in npcs)
                {
                    npc.GetComponentInChildren<Animator>().SetBool("Running", true);
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
                if (!playing && panelsTime <= timeLimit)
                {

                    // Play button press sound
                    audioSource.pitch = 3;
                    audioSource.volume = 1;
                    GetComponent<AudioSource>().Play();

                    playing = true;
                    panelUsed = true;
                }
            }

        }
    }
}
