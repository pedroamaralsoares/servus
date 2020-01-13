using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelTitleScreenManager : MonoBehaviour
{
    public int nextLevelIndex;
    public float timeNextSequence = 3f;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.anyKey) {
            ChangeLevel(nextLevelIndex);
        }
    }

    public void ChangeLevel (int levelIndex) {

        StartCoroutine(FadeAudio(levelIndex));

    }



    IEnumerator FadeAudio(int levelIndex) {
 
        float delay = 1f;
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
    
        yield return new WaitForSeconds (timeNextSequence);

        if (GameObject.Find("Basic Canvas")) {
            Animator canvasImageAnimator = GameObject.Find("Basic Canvas").transform.Find("Image").GetComponent<Animator>();
            canvasImageAnimator.SetBool("Dying",true);
        }
        
        yield return new WaitForSeconds (1.2f);

        SceneManager.LoadSceneAsync(levelIndex);
 
    }
}
