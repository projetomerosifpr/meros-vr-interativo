
using UnityEngine;

public class DisableNet : MonoBehaviour
{
    Cloth clothComponent;
    MeshCollider meshCollider;
    ClothSkinningCoefficient[] coefficients;

    void Start()
    {
        clothComponent = GetComponent<Cloth>();
        meshCollider = GetComponent<MeshCollider>();
        coefficients = clothComponent.coefficients;
    }
    
    public void CutNet()
    {
        for (int i = 0; i < coefficients.Length; i++)
        {
            coefficients[i].maxDistance = Mathf.Infinity;
        }
        clothComponent.coefficients = coefficients;
        meshCollider.enabled = false;

        Destroy(this.gameObject);
    }
}
