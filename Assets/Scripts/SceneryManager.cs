using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryManager : MonoBehaviour
{

    public AudioClip clip;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio (AudioClip newClip) {
        clip = newClip;
        source.clip = clip;
        source.Play();
    }

    public void StopAudio () {
        source.Stop();
    }
}
