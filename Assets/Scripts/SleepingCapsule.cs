using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingCapsule : MonoBehaviour
{

    public Material basicMaterial;
    public Material neonMaterial;

    private MeshRenderer meshRenderer;

    public bool activated = true;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = neonMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMaterial () {
        if (activated) {
            meshRenderer.material = neonMaterial;
            transform.Find("AreaLight").gameObject.SetActive(true);
        }
        else {
            meshRenderer.material = basicMaterial;
            transform.Find("AreaLight").gameObject.SetActive(false);
        }
    }
}
