using UnityEngine;

public class ShaderCollisionHandler : MonoBehaviour
{
    private Material targetMaterial;
    
    // Shader property names must match your Shader Graph string references
    private static readonly int CollisionPointID = Shader.PropertyToID("_CollisionPoint");

    void Start()
    {
        // Cache the material instance on this object to avoid modifying the original asset file
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            targetMaterial = renderer.material;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (targetMaterial == null) return;

        // Grab the exact contact point of the first physics intersection point
        Vector3 hitPoint = collision.contacts[0].point;

        // Feed the Vector3 coordinates right into your custom URP shader
        targetMaterial.SetVector(CollisionPointID, hitPoint);
    }
}
