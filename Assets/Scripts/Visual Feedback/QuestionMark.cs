using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TriggerDroneSound());
    }

    public IEnumerator TriggerDroneSound()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("SOUND_DroneTrigger").GetComponent<AudioSource>().Play();
        Debug.Log("HI");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0,0,0), 1.5f * Time.deltaTime);
    }
}
