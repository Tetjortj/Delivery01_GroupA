using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Coin : MonoBehaviour
{
    public int value = 5;
    public static Action<Coin> OnCollisionCoin;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            OnCollisionCoin?.Invoke(this);
            Destroy(gameObject);
        }
    }

}
