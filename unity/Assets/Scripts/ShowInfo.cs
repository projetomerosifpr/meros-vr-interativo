using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    [SerializeField] private GameObject info;
    private RawImage infoImage;

    [SerializeField] private Texture cardTexture;
    
    void Start()
    {
        infoImage = info.GetComponent<RawImage>();
    }

    public void ShowInfoOnVision()
    {
        info.SetActive(true);
        infoImage.texture = cardTexture;
    }

    public void HideInfoOutVision()
    {
        info.SetActive(false);  
    }
}
