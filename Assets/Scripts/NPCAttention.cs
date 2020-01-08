using UnityEngine;

public class NPCAttention : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<NPCControl>().Wonder = true;
            //StartCoroutine(other.gameObject.GetComponent<NPCState>().TriggerStateAlert());
        }
    }
}
