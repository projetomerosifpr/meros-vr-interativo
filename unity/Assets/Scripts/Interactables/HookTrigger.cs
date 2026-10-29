using System.Collections;
using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] anzois;

    private bool isTriggered;

    private void Start()
    {
        isTriggered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(isTriggered) return;

        if(other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject anzol in anzois)
            {
                anzol.SetActive(true);
            }

            isTriggered = true;
        }
    }
}
    