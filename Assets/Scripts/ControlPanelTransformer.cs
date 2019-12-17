using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelTransformer : MonoBehaviour
{
    public GameObject Modifiable;
    public Mesh[] Meshes;
    public int NoMeshes;
    public int CurrentMesh = 0;

    public SceneryManager SceneryManager;
    public AudioClip SceneryClip;
    private AudioSource AudioSource;

    public float TimeLimit = 0.5f;
    public bool PanelUsed;
    public bool Playing;
    private float PanelsTime;

    public Material RunMaterial;
    public Material StartMaterial;
    private MeshRenderer MeshRenderer;

    void Start()
    {  
        PanelsTime = 0;
        MeshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer.material = StartMaterial;
        Playing = false;

        Modifiable.GetComponent<SkinnedMeshRenderer>().sharedMesh = Meshes[CurrentMesh++ % NoMeshes];
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Playing)
        {
            if (PanelUsed) PanelsTime += Time.deltaTime;

            MeshRenderer.material = RunMaterial;

            if (PanelsTime > TimeLimit)
            {
                Playing = false;

                AudioSource.pitch = 1f;
                AudioSource.volume = 0.8f;
                AudioSource.Play();

                PanelsTime = 0;
            }
        }

        else
        {
            MeshRenderer.material = StartMaterial;
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
            if ((Input.GetKeyDown("q") || Input.GetButtonDown("Fire1")) && SceneryManager != null)
            {
                if (!Playing && PanelsTime <= TimeLimit)
                {
                    AudioSource.pitch = 3;
                    AudioSource.volume = 1;
                    GetComponent<AudioSource>().Play();

                    Playing = true;
                    PanelUsed = true;

                    Modifiable.GetComponent<SkinnedMeshRenderer>().sharedMesh = Meshes[CurrentMesh++ % NoMeshes];
                }
            }
        }
    }
}
