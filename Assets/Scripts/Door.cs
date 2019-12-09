using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool opened = false;

    public Vector3 changePos;
    public Vector3 defaultPos;


    public Material runMaterial;
    public Material blockedMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;
    
    void Start()
    {
        defaultPos = transform.position;
        changePos = new Vector3(defaultPos.x + changePos.x, defaultPos.y + changePos.y, defaultPos.z + changePos.z);

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;

        StartCoroutine(TriggerWrong());
    }


    void Update()
    {
        if (opened) {
            transform.position = Vector3.Lerp(transform.position, changePos, 6 * Time.deltaTime);
            meshRenderer.material = runMaterial;
        }
        else {
            transform.position = Vector3.Lerp(transform.position, defaultPos, 6 * Time.deltaTime);
            meshRenderer.material = startMaterial;
        }
        
    }


    public IEnumerator TriggerWrong()
    {
        meshRenderer.material = blockedMaterial;
        // Also play a sound!
        yield return new WaitForSeconds (1f);
        meshRenderer.material = startMaterial;

    }
}
