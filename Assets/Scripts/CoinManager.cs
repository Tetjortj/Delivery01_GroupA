using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Evento que notifica el valor de la moneda recogida
    public static event Action<int> OnCoinCollectedEvent;

    public static void OnCoinCollected(int coinValue)
    {
        // Invocar el evento y notificar a los observadores
        OnCoinCollectedEvent?.Invoke(coinValue);
    }
}
