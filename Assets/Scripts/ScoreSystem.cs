using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public int Score;
    public static Action<int> OnScoreUpdated;

    private void OnEnable() {
        Coin.OnCollisionCoin += UpdateScore;
    }
    private void OnDisable() {
        Coin.OnCollisionCoin += UpdateScore;
    }

    private void UpdateScore(Coin coin) {
        Score += coin.value;
        OnScoreUpdated?.Invoke(Score);
    }
}

