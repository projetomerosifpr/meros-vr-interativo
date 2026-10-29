using UnityEngine;

public class MoveJoy : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = Camera.main.transform.forward * Input.GetAxis("Vertical") * speed;
        transform.Translate(move * Time.deltaTime, Space.Self);
        rb.linearVelocity = Vector3.zero;
    }
}
