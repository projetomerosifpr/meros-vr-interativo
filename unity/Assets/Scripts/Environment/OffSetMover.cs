using UnityEngine;

public class OffSetMover : MonoBehaviour
{
    public Renderer targetRenderer;
    public Vector2 scrollSpeed = new Vector2(0.1f, 0.0f);

    void Update()
    {
        Material mat = targetRenderer.material;
        Vector2 offset = mat.mainTextureOffset;
        offset += scrollSpeed * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
