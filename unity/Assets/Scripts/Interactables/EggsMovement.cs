using UnityEngine;

public class EggsMovement : MonoBehaviour
{
    public float minAmplitudeY = 0.5f;
    public float maxAmplitudeY = 2f;

    public float minAmplitudeZ = 0.5f;
    public float maxAmplitudeZ = 2f;

    public float minSpeed = 0.5f;
    public float maxSpeed = 1.5f;

    private float amplitudeY;
    private float amplitudeZ;
    private float speed;
    private float phaseOffset;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;

        amplitudeY = Random.Range(minAmplitudeY, maxAmplitudeY);
        amplitudeZ = Random.Range(minAmplitudeZ, maxAmplitudeZ);
        speed      = Random.Range(minSpeed, maxSpeed);

        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float t = Time.time * speed + phaseOffset;
        float y = Mathf.Sin(t) * amplitudeY;
        float z = Mathf.Sin(t) * Mathf.Cos(t) * amplitudeZ;
        transform.position = startPos + new Vector3(0, y, z);
    }
}