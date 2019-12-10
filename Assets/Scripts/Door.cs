using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool locked = true;
    public Light lightSource;
    public bool opened = false;

    public Vector3 changePos;
    public Vector3 defaultPos;

    public AudioClip clip_right;
    public AudioClip clip_wrong;
    private AudioSource audioSource;


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

        audioSource = GetComponent<AudioSource>();

    }


    void Update()
    {

        if (locked) { lightSource.intensity = 0; }
        else {
            lightSource.intensity = 1.5f;
        }

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
        audioSource.clip = clip_wrong;
        audioSource.Play();
        yield return new WaitForSeconds (1f);
        meshRenderer.material = startMaterial;

    }

    public void ChangeMaterial(bool _opened) {

        if (/*_opened == false &&*/ locked) return;


        opened = _opened;

        if (opened) {
            meshRenderer.material = runMaterial;
            if (audioSource != null) {
                audioSource.clip = clip_right;
                audioSource.Play();
            }
            
        }
        else {
            meshRenderer.material = startMaterial;
        }
        
    }
}
