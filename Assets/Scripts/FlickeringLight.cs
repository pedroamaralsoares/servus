using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 1.0f;

    private Light light;

    private void Start()
    {
        light = gameObject.GetComponent<Light>();
    }

    void Update()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        light.enabled = true;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        light.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
    }
}
