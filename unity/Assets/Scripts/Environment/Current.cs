using UnityEngine;

public class Current : MonoBehaviour
{
    const string playerTag = "Player";
    private GameObject player;
    private ScreenShakeVR screenShakeEffect;
    [SerializeField] private float currentForce = 2f;
    private bool isPlayerInCurrent = false;

    private void Start()
    {
        screenShakeEffect = Camera.main.GetComponent<ScreenShakeVR>();
    }

    private void Update()
    {
        ApplyCurrentVelocity();
    }

    private void ApplyCurrentVelocity()
    {
        if(!isPlayerInCurrent) return;

        Vector3 move = transform.forward * currentForce;
        player.transform.Translate(move * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            if(player == null)
            {
                player = other.gameObject;
            }
            else
            {
                Debug.Log("Player not found by current");
            }

            screenShakeEffect.Shake(1, 20);

            isPlayerInCurrent = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            isPlayerInCurrent = false;
        }
    }
}
