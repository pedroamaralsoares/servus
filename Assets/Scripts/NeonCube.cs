using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonCube : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rigidbody;
    public bool activated;

    public bool domesticated;
    public bool domesticatedTouched;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    public bool orbit;
    private float[] randomPosVariation;

    public Material basicMaterial;
    public Material neonMaterial;

    public Material domesticatedMaterial;

    public Color neonColor;
    public Color domesticatedColor;

    private MeshRenderer meshRenderer;
    private SkinnedMeshRenderer sMeshRenderer;

    public bool canBeDraggable;
    public bool timeCycle = true;

    public Transform connectionPrefab;


    private Transform myLed;

    public bool skinned;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;

        if (!skinned)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = neonMaterial;
        }
        else
        {
            sMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            sMeshRenderer.material = neonMaterial;
        }

        randomPosVariation = new float[3];
        randomPosVariation[0] = Random.Range(-0.3f,0.3f);
        randomPosVariation[1] = Random.Range(-0.3f,0.3f);
        randomPosVariation[2] = Random.Range(-0.3f,0.3f);

        myLed = transform.Find("Led");
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        timeCycle = false;
        yield return new WaitForSeconds(waitTime);
        timeCycle = true;

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

            Vector3 orbitVector = Vector3.zero;
            if (orbit) {
                orbitVector = new Vector3(Mathf.PingPong(Time.time * 0.1f, randomPosVariation[0]),
                                                  Mathf.PingPong(Time.time * 0.1f, randomPosVariation[1]),
                                                  Mathf.PingPong(Time.time * 0.1f, randomPosVariation[2])
                                                );
            }
            
            transform.position = Vector3.Lerp(transform.position,
                                initialPosition + orbitVector,
                                6*Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation,initialRotation,6*Time.deltaTime);

            if (!skinned)
                transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", neonColor *  Mathf.Lerp (0.3f, 1.75f, Mathf.PingPong(Time.time * 0.5f, 1)));
            else
                transform.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", neonColor * Mathf.Lerp(0.3f, 1.75f, Mathf.PingPong(Time.time * 0.5f, 1)));

            gameObject.tag = "Untagged";

            if (timeCycle) {
                StartCoroutine(WaitAndPrint(2));
            }

            if (myLed)
                if (!skinned)
                {
                    myLed.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * Mathf.Lerp(0.4f, 1.75f, Mathf.PingPong(Time.time * 1.5f, 1)));
                }
                else
                {
                    myLed.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", Color.white * Mathf.Lerp(0.4f, 1.75f, Mathf.PingPong(Time.time * 1.5f, 1)));
                }
        }
        else {
            // no power, gravity impacts. the object will fall; it will be draggable
            rigidbody.useGravity = true;

            if (canBeDraggable) {
                gameObject.tag = "Draggable";
            }

            if (!skinned)
            {
                transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", neonColor * 0);

                if (myLed)
                {
                    myLed.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white * 0);
                }
            }

            else
            {
                transform.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", neonColor * 0);

                if (myLed)
                {
                    myLed.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", Color.white * 0);
                }
            }
        }

        if (transform.parent == null) {
            domesticatedTouched = false;
        }

        if (domesticated && domesticatedTouched) {

            Color lerpedColor = Color.Lerp(domesticatedColor, neonColor, Mathf.PingPong(Time.time * 1f, 1f));

            if (!skinned) 

                transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", lerpedColor *  Mathf.Lerp (1f, 1.75f, Mathf.PingPong(Time.time * 0.5f, 1)));

                if (myLed) {
                    myLed.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white *  Mathf.Lerp (0.4f, 1.75f, Mathf.PingPong(Time.time * 1.5f, 1)));
                }

            else
            {
                transform.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", lerpedColor * Mathf.Lerp(1f, 1.75f, Mathf.PingPong(Time.time * 0.5f, 1)));

                if (myLed)
                {
                    myLed.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", Color.white * Mathf.Lerp(0.4f, 1.75f, Mathf.PingPong(Time.time * 1.5f, 1)));
                }
            }

        }
    }

    public void SwitchMaterial () {

        if (activated) {
            if (!skinned)
                meshRenderer.material = neonMaterial;
            else
                sMeshRenderer.material = neonMaterial;
        }
        /*
        else if (domesticated) {
            meshRenderer.material = neonMaterial;
        }
        else {
            meshRenderer.material = basicMaterial;
        }
        */

    }
}
