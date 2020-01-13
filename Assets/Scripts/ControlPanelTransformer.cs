using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelTransformer : MonoBehaviour
{
    public GameObject Modifiable;
    public Mesh[] Meshes;
    public Vector3[] Sizes;

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

    public Material turnedOffMaterial;

    private MeshRenderer MeshRenderer;

    private int meshTypeNumber = 0;

    public bool unlocked = false;
    

    void Start()
    {  
        PanelsTime = 0;
        MeshRenderer = GetComponent<MeshRenderer>();
        MeshRenderer.material = turnedOffMaterial;
        Playing = false;

        meshTypeNumber = CurrentMesh++ % NoMeshes;
        Modifiable.GetComponent<SkinnedMeshRenderer>().sharedMesh = Meshes[meshTypeNumber];
        Modifiable.GetComponent<MeshCollider>().sharedMesh = Meshes[meshTypeNumber];
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

        else if (unlocked)
        {
            MeshRenderer.material = StartMaterial;
        }

        else {
            MeshRenderer.material = turnedOffMaterial;
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
        if (!unlocked) return;

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

                    meshTypeNumber = CurrentMesh++ % NoMeshes;
                    Modifiable.GetComponent<SkinnedMeshRenderer>().sharedMesh = Meshes[meshTypeNumber];
                    Modifiable.GetComponent<MeshCollider>().sharedMesh = Meshes[meshTypeNumber];
                    Modifiable.transform.localScale = Sizes[meshTypeNumber];
                }
            }
        }
    }
}
