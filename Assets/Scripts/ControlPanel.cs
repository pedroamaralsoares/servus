using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    public SceneryManager sceneryManager;
    public AudioClip clip;
    public float timeLimit = 3f;

    private bool panelUsed;

    public static bool playing;
    static float panelsTime;
    static bool locked;
    public Material runMaterial;
    public Material blockedMaterial;

    private MeshRenderer meshRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        panelsTime = 0;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update () {
        if (playing) {
            if (panelUsed) panelsTime += Time.deltaTime;
            meshRenderer.material = runMaterial;

            if (panelsTime > timeLimit) {
                playing = false;
                locked = true;
            }
        }
        
        else if (locked) {
            meshRenderer.material = blockedMaterial;
            sceneryManager.StopAudio();
        }

        else {
            //meshRenderer.material = normalMaterial;
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
            if (Input.GetKeyDown("q") && sceneryManager != null) {
                if (!playing) {
                    sceneryManager.PlayAudio(clip);
                    playing = true;
                    panelUsed = true;
                }
                else {
                    sceneryManager.StopAudio();
                    playing = false;
                }
            }
        }
    }
}
