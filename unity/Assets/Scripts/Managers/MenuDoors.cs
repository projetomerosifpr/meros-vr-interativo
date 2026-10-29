using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoors : MonoBehaviour
{
    public string levelSceneName;
    [SerializeField] private SceneController sceneController;
    
    private bool isLooking = false;
    [HideInInspector] public bool sceneTransitionDone;

    void Update() {
        if(!isLooking) return;
        
        if(Input.GetButtonDown("Jump"))
        {
            StartLevel();        
        }
    }

    public void IsLooking(bool pointerEnter) 
    {
        isLooking = pointerEnter;
    }

    private void StartLevel() 
    {
        StartCoroutine(sceneController.LoadScene(levelSceneName));
    }
}
