using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject NPCprefab;
    public Transform StartPosition;

    private GameObject npcInstance;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("NPCSpawner", 3.0f, 20.0f);
        }
    }

    private void NPCSpawner()
    {
        if (npcInstance != null) Destroy(npcInstance);
        npcInstance = Instantiate(NPCprefab, StartPosition.position, StartPosition.rotation);
    }
}

