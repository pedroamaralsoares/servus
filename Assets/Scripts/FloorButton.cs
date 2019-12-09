using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{

    private bool activated;

    private Vector3 initialPos;
    public Material runMaterial;
    public Material blockedMaterial;
    public Material startMaterial;

    private MeshRenderer meshRenderer;

    public Door door;


    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = startMaterial;

        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated) {
            transform.position = Vector3.Lerp(transform.position, new Vector3 (initialPos.x, initialPos.y - 0.15f, initialPos.z),6*Time.deltaTime);
        }
        else {
            transform.position = Vector3.Lerp(transform.position, initialPos, 6*Time.deltaTime);

        }
    }


    void OnTriggerEnter (Collider collider) {
        Trigger(collider);
    }
    void OnTriggerStay(Collider collider)
    {
        Trigger(collider);
    }

    void OnTriggerExit() {
        meshRenderer.material = startMaterial;
        activated = false;
        door.opened = false;
    }

    void Trigger (Collider collider) {
        if (collider.gameObject.tag == "Draggable")
        {
             meshRenderer.material = runMaterial;
             activated = true;
             door.opened = true;
        }
    }
}
