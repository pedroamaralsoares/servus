using UnityEngine;

public class NPCAttention : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Sound detected");
            StartCoroutine(other.gameObject.GetComponent<NPCState>().TriggerStateAlert());
        }
    }
}
