using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualFloor : MonoBehaviour
{
    bool triggered;
    Vector3 latestPlayerPos;
    float timeToRest = 0.5f; // time to rest since last sound emmited (3 sec for each sample)
    public Transform soundSamplePrefab;

    public AudioClip[] clips;
    private int numClips;

    // Start is called before the first frame update
    void Start()
    {
        latestPlayerPos = new Vector3(0,0,0);
        numClips = clips.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToRest >= 0f && timeToRest < 3f) timeToRest += Time.deltaTime;
    }

    void Awake()
    {
        triggered = false;
    }

    void OnTriggerEnter (Collider collider) {

        latestPlayerPos = collider.transform.position;

        if (collider.gameObject.tag == "Player")
        {
            Transform newSoundObject = Instantiate(soundSamplePrefab, latestPlayerPos, collider.transform.rotation);
            newSoundObject.GetComponent<AudioSource>().clip = randomClip();
            newSoundObject.GetComponent<AudioSource>().Play();
            timeToRest = 0f;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (latestPlayerPos == collider.transform.position) return;
            latestPlayerPos = collider.transform.position;
            //Debug.Log(latestPlayerPos);

            if (timeToRest >= 0.5f) {
                Transform newSoundObject = Instantiate(soundSamplePrefab, latestPlayerPos, collider.transform.rotation);
                newSoundObject.GetComponent<AudioSource>().clip = randomClip();
                newSoundObject.GetComponent<AudioSource>().Play();
                timeToRest = 0f;
            }
        }
    }

    void OnTriggerExit (Collider collider) {

    }

    AudioClip randomClip () {
        return clips[Random.Range(0,numClips-1)];
    }
}
