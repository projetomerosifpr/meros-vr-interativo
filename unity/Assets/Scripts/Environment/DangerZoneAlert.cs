using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class DangerZoneAlert : MonoBehaviour
{
    public ShowInfo info;

    public void Start()
    {
        info = gameObject.GetComponent<ShowInfo>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ProgressionControler._instance.IsInDangerZone++;
            
            if(info == null)
            {
                ProgressionControler._instance.IsInPolution = true;
                UnityEngine.Debug.Log("Is in pollution");
                return;
            }
            if(info != null)
            {
                info.ShowInfoOnVision();
                return;
            }
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other == null)return;
        
            if(other.gameObject.CompareTag("Player"))
            {
                ProgressionControler._instance.IsInDangerZone--;

            if(info == null)
            {
                ProgressionControler._instance.IsInPolution = true;
                return;
            }
            if(info != null)
            {
                info.HideInfoOutVision();
                return;
            }
                
            }
    }
}
