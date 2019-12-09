using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttach : MonoBehaviour
{
    public int NumberOfNPCs;
    private int Count = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            other.gameObject.transform.parent = this.transform;

            Animator anim = this.transform.parent.GetComponent<Animator>();
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Count++;
            other.gameObject.transform.parent = null;

            GameObject closestDrone = other.gameObject.GetComponent<NPCControl>().FindClosestDrone();
            if (closestDrone != null)
            {
                closestDrone.GetComponent<DroneNavAgent>().tracking = true;
                closestDrone.GetComponent<DroneNavAgent>().npcTargets.Add("npc" + other.gameObject.GetComponent<NPCControl>().ID, other.gameObject.transform);
            }
        }

        if (Count == NumberOfNPCs)
        {
            Animator anim = this.transform.parent.GetComponent<Animator>();
            anim.SetBool("Down", false);
            anim.SetBool("Up", true);
            Count = 0;
        }
    }
}
