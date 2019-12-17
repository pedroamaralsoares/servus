using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonCube : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rigidbody;
    public bool activated;

    public bool domesticated;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    public Material basicMaterial;
    public Material neonMaterial;

    public Material domesticatedMaterial;

    private MeshRenderer meshRenderer;

    public bool canBeDraggable;
    public bool timeCycle = true;

    public Transform connectionPrefab;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = neonMaterial;
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        timeCycle = false;
        yield return new WaitForSeconds(waitTime);
        timeCycle = true;
        print("Coroutine ended: " + Time.time + " seconds");

        if (connectionPrefab) {
            Instantiate(connectionPrefab, transform.position, transform.rotation);
            connectionPrefab.GetComponent<ConnectionPathElement>().target = initialPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && domesticated == false) {
            // no gravity, it will go to its original position/state
            rigidbody.useGravity = false;
            transform.position = Vector3.Lerp(transform.position,initialPosition,6*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation,initialRotation,6*Time.deltaTime);

            gameObject.tag = "Untagged";

            if (timeCycle) {
                StartCoroutine(WaitAndPrint(2));
            }

        }
        else {
            // no power, gravity impacts. the object will fall; it will be draggable
            rigidbody.useGravity = true;

            if (canBeDraggable) {
                gameObject.tag = "Draggable";
            }
        }

        if (domesticated) {
            meshRenderer.material = domesticatedMaterial;
        }
    }

    public void SwitchMaterial () {
        if (activated) {
            meshRenderer.material = neonMaterial;
        }
        else {
            meshRenderer.material = basicMaterial;
        }

        if (domesticated) {
            meshRenderer.material = domesticatedMaterial;
        }

    }
}
