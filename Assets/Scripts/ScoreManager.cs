using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    

    public int score = 0;

    public int best = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    private const string SCORE_KEY = "SavedScore";

    private const string BEST_KEY = "BestScore";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadScore();
        UpdateScoreText();
    }
    public int GetBestScore()
    {
        return PlayerPrefs.GetInt(BEST_KEY);
    }

    public void AddScore(int amount)
    {
        score += amount;
        CheckBestScore();
        UpdateScoreText();
        SaveScore();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Счёт: " + score;
        if (bestScoreText != null)
            bestScoreText.text = "Рекорд: " + best;
    }

    public void ResetScore()
    {
        CheckBestScore();
        score = 0;
        UpdateScoreText();
        PlayerPrefs.DeleteKey(SCORE_KEY);
    }

    public void CheckBestScore()
    {
        best = PlayerPrefs.GetInt(BEST_KEY);
        if (score > best)
        {
            best = score;
            PlayerPrefs.SetInt(BEST_KEY, best);
            PlayerPrefs.Save();
        }
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            score = PlayerPrefs.GetInt(SCORE_KEY);
        }
        else
        {
            score = 0;
        }
        best =  PlayerPrefs.GetInt(BEST_KEY);
    }
}

