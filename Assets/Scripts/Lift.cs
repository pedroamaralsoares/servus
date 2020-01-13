using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float oldHeight;
    public float newHeight;
    public float speed;

    private bool move = true;
    private bool back = false;
    public bool canMove = false;

    public Material regularGrid;
    public Material neonGrid;


    public Transform[] linesWhileActivated;
    public Transform[] linesWhileDoorActivated; 


    public AudioSource audioSource;

    public void Start()
    {
        oldHeight = transform.position.y;

        foreach (Transform line in linesWhileActivated) {
            line.gameObject.SetActive(false);
        }
        foreach (Transform line in linesWhileDoorActivated) {
            line.gameObject.SetActive(true);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (canMove)
        {
            transform.GetComponent<MeshRenderer>().material = neonGrid;

            if (move)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, newHeight, transform.position.z), step);

                if (Mathf.Approximately(transform.position.y, newHeight))
                {
                    StartCoroutine(MoveDown());
                }

            }
            if (back)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, oldHeight, transform.position.z), step);

                if (Mathf.Approximately(transform.position.y, oldHeight))
                {
                    StartCoroutine(MoveUp());
                }

            }

            foreach (Transform line in linesWhileActivated) {
                line.gameObject.SetActive(true);
            }
            foreach (Transform line in linesWhileDoorActivated) {
                line.gameObject.SetActive(false);
            }
            
        }

        else {
            transform.GetComponent<MeshRenderer>().material = regularGrid;


            foreach (Transform line in linesWhileActivated) {
            line.gameObject.SetActive(false);
            }
            foreach (Transform line in linesWhileDoorActivated) {
                line.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator MoveUp()
    {
        audioSource.Stop();
        yield return new WaitForSeconds(3f);
        back = false;
        move = true;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private IEnumerator MoveDown()
    {
        audioSource.Stop();
        yield return new WaitForSeconds(3f);
        back = true;
        move = false;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
