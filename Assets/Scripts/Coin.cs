using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Coin : MonoBehaviour
{
    public int value;
    public static Action<Coin> OnCollisionCoin;

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            OnCollisionCoin?.Invoke(this);
            //Update Ui
            
            Destroy(gameObject);
            ScoreManager.instance.AddPoint();
        }
    }

}
