using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject NPCprefab;
    public Transform StartPosition;

    private GameObject npcInstance;

    public float spawnTimeRepeatPeriod = 15.0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("NPCSpawner", 0.0f, spawnTimeRepeatPeriod);
        }
    }

    private void NPCSpawner()
    {
        if (npcInstance != null) Destroy(npcInstance);
        npcInstance = Instantiate(NPCprefab, StartPosition.position, StartPosition.rotation);
    }
}

