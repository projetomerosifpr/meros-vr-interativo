using UnityEngine;

public class NpcFish : MonoBehaviour
{
    private Vector3 TargetPos, speed, inicialPosition;
    public float time = 2f, radius = 2f, targetTime = 10f;
    private int goneCounter = 0, goneLimit = 2;
    private Rigidbody rb;

    void Start()
    {
        inicialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        RandomizePos();
    }

    void Update()
    {
        if (TargetPos != Vector3.zero) MoveAround();
        rb.linearVelocity = Vector3.zero;
    }

    private void MoveAround()
    {
        LookAtTarget();
        transform.position = Vector3.SmoothDamp(transform.position, TargetPos, ref speed, 3);
        if (Vector3.Distance(gameObject.transform.position, TargetPos) <= 0.2f || targetTime <= 0) RandomizePos();
        targetTime -= Time.deltaTime;

    }
    
    private void RandomizePos()
    {
        if (goneCounter >= goneLimit)
        {
            TargetPos = inicialPosition;
            goneCounter = 0;
        }
        else
        {
            TargetPos = new Vector3(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius), transform.position.z + Random.Range(-radius, radius));
        }
        goneCounter++;
        targetTime = 10f;
    }

    void LookAtTarget()
    {
        if (Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(speed), 0.3f * Time.deltaTime) != new Quaternion(0, 0, 0, 0))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(speed), 0.8f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 awayDir = -transform.position.normalized;
            TargetPos = transform.position + awayDir * radius;
        }
        if (collision.gameObject.CompareTag("Tarrain"))
        {
            TargetPos = inicialPosition;
            goneCounter = 0;
        }
    }

}
