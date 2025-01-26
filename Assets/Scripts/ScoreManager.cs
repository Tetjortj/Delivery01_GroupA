
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // s'instacia l'objecte perque es pugui utilitzar a la coin
    public static ScoreManager instance;

    // es crea el int qu eomplira el text score i el text score
    public Text scoreText;
    public Text highScoreText;
    int highScore = 0;
    int score = 0;

    public void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //S'actualitza el highScore i el score a 0
        highScore = PlayerPrefs.GetInt("highScore", 0);
        scoreText.text = score.ToString() + " POINTS";
        highScoreText.text = "HIGHSCORE: " + highScore.ToString();
    }

    // Update is called once per frame
    public void AddPoint()
    {
        //es puja punt cada cop que es recolecta una fruita i s'actualitza el highScore
        score += 1;
        scoreText.text = score.ToString() + " POINTS";
        if (highScore < score)
        {
            PlayerPrefs.GetInt("highScore", score);
        }

    }
}
