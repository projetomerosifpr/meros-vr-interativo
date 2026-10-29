using UnityEngine;

public class Crab : MonoBehaviour
{
    
    [SerializeField] private DisableNet Net;
    [SerializeField] private GameObject plane;
    [SerializeField] private Transform TargetPos;
    [SerializeField] private Transform Player;
    [SerializeField] private int index;
    
    private Vector3 speed;
    private bool isFreed = false;
    private bool isOnPosition = false;
    void Update()
    {
	    if(ProgressionControler._instance.GetInteractedWithCrab(index) && Input.GetButtonDown("Jump"))
        {
            if(Net!=null) Net.CutNet();
            plane.SetActive(true);
            isFreed = true;
        }
        if(isFreed)
        {
            transform.position = Vector3.SmoothDamp(transform.position, TargetPos.position, ref speed, 3);
            if((gameObject.transform.position - TargetPos.position).magnitude < 0.1f)
            {
                isOnPosition = true;
                isFreed = false;
            }
        }
        if(isOnPosition) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position), 0.8f * Time.deltaTime);
        }
    }

    public void InteractCrab()
    {
	    ProgressionControler._instance.SetInteractedWithCrab(true, index);
    }

    public void OnPointerCrab()
    {
	    if(!Net) return;
	    ProgressionControler._instance.SetInteractedWithCrab(true, index);
    }

    public void OutPointerCrab()
    {
	    if(!Net)return;
        ProgressionControler._instance.SetInteractedWithCrab(false, index);   
    }
}