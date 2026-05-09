using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public AudioClip dropSound;
    public AudioClip[] mergeSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    public GameOverUI gameOverUI;
    public GameObject gameOverPanel;
    public AdPopupManager adPopupManager;
  

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }
    public void ShowGameOverUI()
    {
        Debug.Log("показал конец игры");
        adPopupManager.SetActive(false);
        int finalScore = ScoreManager.Instance.score;
        int bestScore = ScoreManager.Instance.best;
        gameOverUI.ShowGameOver(finalScore, bestScore);

        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }
    public void RestartGame()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        gameOverPanel.SetActive(false);
        adPopupManager.SetActive(true);
        ScoreManager.Instance.ResetScore();
    }
    public void RetryGame()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        int halfCount = balls.Length / 2;

        for (int i = 0; i < halfCount; i++)
        {
            Destroy(balls[i]);
        }
        YG2.InterstitialAdvShow();
        gameOverPanel.SetActive(false);
        adPopupManager.SetActive(true);
        ScoreManager.Instance.CheckBestScore();
    }
    
    public AudioClip GetRandomMergeSound()
    {
        AudioClip merge = mergeSound[UnityEngine.Random.Range(0, mergeSound.Length)];
        return merge;
    }
}