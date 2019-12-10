using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointEndLevel : MonoBehaviour
{
    public int nextLevelIndex;
    public LevelDebugManager levelDebugManager;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" || collider.tag == "Draggable") {
            levelDebugManager.ChangeLevel(nextLevelIndex);
        }
    }
}
