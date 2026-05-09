using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using YG;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button restartButton;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI bestScoretext;

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOver(int score, int bestScore)
    {
        gameOverPanel.SetActive(true);

        retryButton.interactable = true;
        restartButton.gameObject.SetActive(false);

        finalScoreText.text = "Счёт: " + score.ToString();
        bestScoretext.text = "Рекорд: " + bestScore.ToString();

        StartCoroutine(ShowRestartButtonWithDelay(3f));
    }

    private IEnumerator ShowRestartButtonWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        restartButton.gameObject.SetActive(true);
        restartButton.interactable = true;
    }
}