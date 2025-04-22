using UnityEngine;
using UnityEngine.UI;

public class ZoneScore : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score.ToString();
    }
}
