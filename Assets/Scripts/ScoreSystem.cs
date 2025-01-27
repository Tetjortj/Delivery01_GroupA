using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    public int score;
    public static Action<int> OnScoreUpdated;

    private void OnEnable() {
        Coin.OnCollisionCoin += UpdateScore;
    }
    private void OnDisable() {
        Coin.OnCollisionCoin += UpdateScore;
    }

    private void UpdateScore(Coin coin) {
        score += coin.value;
        OnScoreUpdated?.Invoke(score);
        if (score == 105)
        {
            SceneManager.LoadScene("Ending");
        }
    }
}

