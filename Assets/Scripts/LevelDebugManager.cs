using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelDebugManager : MonoBehaviour
{
    public int nextLevelIndex;
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
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
