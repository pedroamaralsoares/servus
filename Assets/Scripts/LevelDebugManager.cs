using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelDebugManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("p")) {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void ChangeLevel (int levelIndex) {
        SceneManager.LoadSceneAsync(levelIndex);
    }
}
