using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonCube : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rigidbody;
    public bool activated;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    public Material basicMaterial;
    public Material neonMaterial;

    private MeshRenderer meshRenderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = neonMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            // no gravity, it will go to its original position/state
            rigidbody.useGravity = false;
            transform.position = Vector3.Lerp(transform.position,initialPosition,6*Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation,initialRotation,6*Time.deltaTime);
        }
        else {
            // no power, gravity impacts. the object will fall; it will be draggable
            rigidbody.useGravity = true;
        }
    }

    public void SwitchMaterial () {
        if (activated) {
            meshRenderer.material = neonMaterial;
        }
        else {
            meshRenderer.material = basicMaterial;
        }
    }
}
