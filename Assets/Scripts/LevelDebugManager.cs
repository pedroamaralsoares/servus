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

        StartCoroutine(FadeAudio(levelIndex));

    }

    public void Death () {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("checkpoint_pos");

        if (objs.Length > 0) {

            if (inRestartNewPrefab) {
                objs[0].transform.GetComponent<DontDestroy>().InstantiateNewStuff(inRestartNewPrefab, inRestartNewPrefabPosition, lastCheckpoint);
            }
        }
        StartCoroutine(FadeAudio(SceneManager.GetActiveScene().buildIndex));
        
    }


    IEnumerator FadeAudio(int levelIndex) {
 
        float delay = 1f;
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime*2.5f;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }

        SceneManager.LoadSceneAsync(levelIndex);
 
    }
}
