using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableNet : MonoBehaviour
{
    [SerializeField] private GameObject asdfasdf;
    [SerializeField] private int index;

    void Update()
    {
        if(ProgressionControler._instance.GetInteractedWithCrab(index))asdfasdf.SetActive(true);
    }
}
