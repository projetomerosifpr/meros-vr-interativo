using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTransition : MonoBehaviour
{
    [SerializeField] private int currentLevel, nextLevel; // 0 - Menu; 1 - Mangroove; 2 - Current; 3 - Sea; 4 - Rocks
    private string[] sceneName = {"MainMenu", "Mangroove2.0", "Current", "Sea", "Rocks"};
    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            SceneManager.LoadScene(sceneName[nextLevel]);
        }
    }
}
