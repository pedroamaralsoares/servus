using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
            other.gameObject.GetComponent<NPCControl>().Move = true;
            //other.gameObject.GetComponent<NPCState>().StartWalking();
    }
}
