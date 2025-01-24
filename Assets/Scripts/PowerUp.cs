using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float jumpMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.jumpForce *= jumpMultiplier;
            }

            Destroy(gameObject);
        }
    }
}
