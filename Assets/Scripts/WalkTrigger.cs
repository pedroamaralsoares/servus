using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTrigger : MonoBehaviour
{
    public Transform Waypoint;
    public float WaitPeriod;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<NPCControl>().Move = false;
            other.gameObject.GetComponent<NPCControl>().Float = false;

            StartCoroutine(TriggerWalk(other));
        }
    }

    private IEnumerator TriggerWalk(Collider other)
    {
        yield return new WaitForSeconds(WaitPeriod);
        other.gameObject.GetComponent<NPCControl>().Move = true;
        other.gameObject.GetComponent<NPCControl>().Waypoint = Waypoint;
    }
}
