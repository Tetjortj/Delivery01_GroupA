using UnityEngine;

public class CoinScore : MonoBehaviour
{
    public int coinValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinManager.OnCoinCollected(coinValue);
            Destroy(gameObject);
        }
    }
}
