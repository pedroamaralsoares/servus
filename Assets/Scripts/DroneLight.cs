using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLight : MonoBehaviour
{
    Transform player;
    public bool tracking;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracking) {
            transform.LookAt(player);
        }

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(transform.position,fwd, out hit, 100f))
            if (hit.collider != null && hit.collider.tag == "Player")
                Debug.Log("DIE");
    }
}
