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

    public bool playing;
    private float panelsTime;
    private bool locked;
    public Material runMaterial;
    public Material blockedMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

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
            if (Input.GetKeyDown("q") && sceneryManager != null) {
                if (!playing) {
                    sceneryManager.PlayAudio(clip);
                    playing = true;
                    panelUsed = true;
                }
                //else {
                //    sceneryManager.StopAudio();
                //    playing = false;
                //}
            }
        }
    }
}
