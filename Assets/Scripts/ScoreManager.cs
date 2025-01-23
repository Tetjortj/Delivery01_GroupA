using UnityEngine;
using UnityEngine.UI;


public class NewMonoBehaviourScript : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    private void OnEnable()
    {
        CoinManager.OnCoinCollectedEvent += UpdateScore;
    }

    private void OnDisable()
    {
        CoinManager.OnCoinCollectedEvent -= UpdateScore;
    }

    private void UpdateScore(int coinValue)
    {
        score += coinValue;
        scoreText.text = "Score: " + score;
    }
}
