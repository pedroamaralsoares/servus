using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelDebugManager : MonoBehaviour
{
    public int nextLevelIndex;

    /* Restart checkpoint - save position & objects */
    public Transform inRestartNewPrefab; // object important for future progression (draggable, for example)
    public Vector3 inRestartNewPrefabPosition;

    public Checkpoint lastCheckpoint;
    /* -------------- */

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("p")) {
            ChangeLevel(nextLevelIndex);
        }
    }

    public void ChangeLevel (int levelIndex) {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");
        if (objs.Length > 0) {
            GameObject.Destroy(objs[0]);

        }

        SceneManager.LoadSceneAsync(levelIndex);
    }

    public void Death () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");

        if (objs.Length > 0) {

            if (inRestartNewPrefab) {
                objs[0].transform.GetComponent<DontDestroy>().InstantiateNewStuff(inRestartNewPrefab, inRestartNewPrefabPosition, lastCheckpoint);
            }
        }

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
