using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonDraggable : MonoBehaviour
{

    private bool activated;
    private bool touched;

    private Vector3 initialPos;
    public Material runMaterial;
    public Material blockedMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

    public ControlPanelTransformer[] controlPanelTransformers;

    public Transform[] linesWhileActivated;
    public Transform[] linesWhileDoorActivated; 


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;

        initialPos = transform.position;


        foreach (Transform line in linesWhileActivated) {
            line.gameObject.SetActive(false);
        }
        foreach (Transform line in linesWhileDoorActivated) {
            line.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(touched) {
            transform.position = Vector3.Lerp(transform.position, new Vector3 (initialPos.x, initialPos.y - 0.15f, initialPos.z),6*Time.deltaTime);

        }
        else {
            transform.position = Vector3.Lerp(transform.position, initialPos, 6*Time.deltaTime);

        }
    }


    void OnTriggerEnter (Collider collider) {
        Trigger(collider);
        /*door.StartCoroutine(door.TriggerWrong());
        StartCoroutine(TriggerWrong());*/
    }
    void OnTriggerStay(Collider collider)
    {
        Trigger(collider);
    }

    void OnTriggerExit() {
        meshRenderer.material = startMaterial;
        touched = false;
        activated = false;
        
        //door.ChangeMaterial(false);
        for (int i = 0; i < controlPanelTransformers.Length; i++) {
                controlPanelTransformers[i].unlocked = false;
        }

        foreach (Transform line in linesWhileActivated) {
            line.gameObject.SetActive(false);
        }
        foreach (Transform line in linesWhileDoorActivated) {
            line.gameObject.SetActive(true);
        }
    }

    void Trigger (Collider collider) {
        
        touched = true;
        if (collider.gameObject.tag == "Draggable")
        {
            meshRenderer.material = runMaterial;
            activated = true;
            for (int i = 0; i < controlPanelTransformers.Length; i++) {
                controlPanelTransformers[i].unlocked = true;
            }

            foreach (Transform line in linesWhileActivated) {
                line.gameObject.SetActive(true);
            }
            foreach (Transform line in linesWhileDoorActivated) {
                line.gameObject.SetActive(false);
            }
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
