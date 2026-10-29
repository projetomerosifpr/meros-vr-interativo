using UnityEngine;
using UnityEngine.SceneManagement;
public class Hook : MonoBehaviour
{
    [SerializeField] private Transform target;
    void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            if (transform.position == target.position) return;
            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.2f);
        }
    }
}