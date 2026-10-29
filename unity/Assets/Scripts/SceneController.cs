using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private float transitionDuration = 0.00001f;

    public IEnumerator LoadScene(string levelSceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelSceneName);

        asyncLoad.allowSceneActivation = false;
        yield return StartCoroutine(SceneFadeTransition());
        asyncLoad.allowSceneActivation = true;

        while(!asyncLoad.isDone) // finish loading the scene
        {
            yield return null;
        }
    }
    
    private IEnumerator SceneFadeTransition()
    {
        float elapsedTime = 0f;
        float elapsedPercentage = 0f;
        
        while(elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / transitionDuration;
            // fadeImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);
            
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }

}
