using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    Checkpoint savedCheckpointVirtual;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    


    public void InstantiateNewStuff(Transform inRestartNewPrefab, Vector3 inRestartNewPrefabPosition, Checkpoint checkpoint) {
        
        StartCoroutine(InstantiateNewStuffTimed(inRestartNewPrefab,inRestartNewPrefabPosition, checkpoint));
    }

    public IEnumerator InstantiateNewStuffTimed(Transform inRestartNewPrefab, Vector3 inRestartNewPrefabPosition, Checkpoint checkpoint)
    {
        /* change camera parameters - like Trigger() from Checkpoint.cs */

        if (checkpoint) {
            savedCheckpointVirtual = new Checkpoint();
            savedCheckpointVirtual.newOffsetFromFocusPoint = checkpoint.newOffsetFromFocusPoint;
            savedCheckpointVirtual.newDistance = checkpoint.newDistance;
            savedCheckpointVirtual.fastX = checkpoint.fastX;
        }
        
        var newOffsetFromFocusPoint = savedCheckpointVirtual.newOffsetFromFocusPoint;
        var newDistance = savedCheckpointVirtual.newDistance;
        var fastX = savedCheckpointVirtual.fastX;
        /* ====================================== */

        yield return new WaitForSeconds (1f);
        var inst = Instantiate(inRestartNewPrefab);
        inst.position = new Vector3(inRestartNewPrefabPosition.x,GameObject.Find("Character").transform.position.y,GameObject.Find("Character").transform.position.z);
        
        LevelDebugManager levelDebugManager = GameObject.Find("LevelDebugManager").transform.GetComponent<LevelDebugManager>();
        levelDebugManager.inRestartNewPrefab = inRestartNewPrefab;
        levelDebugManager.inRestartNewPrefabPosition = inRestartNewPrefabPosition;


        
        var gameCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        var cameraTarget = GameObject.Find("CameraTarget").transform;

        cameraTarget.GetComponent<CameraTarget>().offsetFromFocusPoint = newOffsetFromFocusPoint;

        if (newDistance > 0) {
                
            gameCamera.GetComponent<CameraMove>().distance = newDistance;
        }

        cameraTarget.GetComponent<CameraTarget>().fastX = fastX;
        gameCamera.GetComponent<CameraMove>().isRotY = fastX;

    }
}
