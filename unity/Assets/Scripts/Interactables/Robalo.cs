using UnityEngine;

public class Robalo : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    public Transform player;
    private Vector3 axis = new Vector3(0, 1, 0);
    private Vector3 direction = new Vector3(-1, 0, 0);

    public float linerVelocity = 0.5f;
    public float angularVelocity = 20f;

    private bool isInInitialPos = true;
    public void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if(!ProgressionControler._instance.IsInPolution)MoveRobalo();
    }

    void MoveRobalo()
    {
        float fixedDelta = Time.fixedDeltaTime;
        int dangerZone = ProgressionControler._instance.IsInDangerZone;

        Debug.DrawRay(transform.position, (player.position - transform.position) * 2, Color.red);

        if (dangerZone == 0 && isInInitialPos)
        {
            transform.Rotate(axis * angularVelocity * fixedDelta);
        }

        if (dangerZone > 0)
        {
            isInInitialPos = false;
            
            Vector3 targetDir = player.position - transform.position;
            if (targetDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDir) * Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * fixedDelta);
            }
        }
        
        if (dangerZone == 0 && !isInInitialPos)
        {
            Vector3 targetDir = initialPosition - transform.position;
            
            if (targetDir.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDir) * Quaternion.Euler(0, 90, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * fixedDelta);
            }
            else
            {
                transform.position = initialPosition;
                transform.rotation = initialRotation;
                isInInitialPos = true;
            }
        }

        transform.Translate(direction * linerVelocity * fixedDelta); 
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().Die();
        }
    }
}