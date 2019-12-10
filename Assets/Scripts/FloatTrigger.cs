using UnityEngine;
using System.Collections;

public class FloatTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<NPCControl>().Float = true;
        }
    }
}
