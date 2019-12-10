using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public SceneryManager sceneryManager;
    public AudioClip sceneryClip;
    public float timeLimit = 3f;

    public bool panelUsed;

    public bool playing;
    private float panelsTime;
    private bool locked;
    public Material runMaterial;
    public Material blockedMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

    public ControlPanel[] panels;

    public Transform particlesRain;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        panelsTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;
        playing = false;
        locked = false;
    }

    void Update () {
        if (playing) {
            if (panelUsed) panelsTime += Time.deltaTime;

            meshRenderer.material = runMaterial;

            if (panelsTime > timeLimit)
            {
                playing = false;
                locked = true;

                
                // Play button press sound
                GetComponent<AudioSource>().pitch = 1f;
                GetComponent<AudioSource>().volume = 0.8f; 
                GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().pitch = 1;
                GetComponent<AudioSource>().volume = 1;
            }
        }
        
        else if (locked) {
            meshRenderer.material = blockedMaterial;
            sceneryManager.StopAudio();
            VirtualInputManager.Instance.Rain = false;

            if (particlesRain) {
                Destroy(particlesRain.gameObject);
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
                    GetComponent<AudioSource>().Play();

                    // Play the new scenary's sound
                    sceneryManager.PlayAudio(sceneryClip);

                    VirtualInputManager.Instance.Rain = true;

                    foreach (ControlPanel cp in panels)
                    {
                        cp.playing = true;
                        cp.panelUsed = true;
                    }

                    if (particlesRain) {
                        particlesRain.gameObject.SetActive(true);
                    }
                    
                }
                
            }
        }
    }
}
