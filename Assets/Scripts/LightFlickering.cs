using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    private Material material;
    public float timeCycle = 2;

    public Color color1;
    public Color color2;


    void Start()
    {
        material = GetComponent<MeshRenderer>().material;    
    }

    void Update()
    {
        Color newColor = Color.Lerp(color1,color2,Mathf.PingPong(Time.time, timeCycle));
        
        material.SetColor("_EmissionColor", newColor);

    }
}
