using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
            other.gameObject.GetComponent<Waypoint>().Walking = true;
    }
}
