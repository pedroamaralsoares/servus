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

    }


    void Update()
    {
        if (opened) {
            transform.position = Vector3.Lerp(transform.position, changePos, 6 * Time.deltaTime);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, defaultPos, 6 * Time.deltaTime);
        }
        
    }


    public IEnumerator TriggerWrong()
    {
        meshRenderer.material = blockedMaterial;
        // Also play a sound!
        yield return new WaitForSeconds (1f);
        meshRenderer.material = startMaterial;

    }

    public void ChangeMaterial(bool _opened) {
        opened = _opened;

        if (opened) {
            meshRenderer.material = runMaterial;
        }
        else {
            meshRenderer.material = startMaterial;
        }
    }
}
