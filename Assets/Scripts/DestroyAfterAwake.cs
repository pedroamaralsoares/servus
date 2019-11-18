using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAwake : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDestroy = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0f) {
            Destroy(this.gameObject);
        }
    }
}
