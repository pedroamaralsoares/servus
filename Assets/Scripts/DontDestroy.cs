using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    


    public void InstantiateNewStuff(Transform inRestartNewPrefab, Vector3 inRestartNewPrefabPosition) {
        
        StartCoroutine(InstantiateNewStuffTimed(inRestartNewPrefab,inRestartNewPrefabPosition));
    }

    public IEnumerator InstantiateNewStuffTimed(Transform inRestartNewPrefab, Vector3 inRestartNewPrefabPosition)
    {
        yield return new WaitForSeconds (1f);
        var inst = Instantiate(inRestartNewPrefab);
        inst.position = new Vector3(inRestartNewPrefabPosition.x,GameObject.Find("Character").transform.position.y,GameObject.Find("Character").transform.position.z);
        
        LevelDebugManager levelDebugManager = GameObject.Find("LevelDebugManager").transform.GetComponent<LevelDebugManager>();
        levelDebugManager.inRestartNewPrefab = inRestartNewPrefab;
        levelDebugManager.inRestartNewPrefabPosition = inRestartNewPrefabPosition;

    }
}
