using UnityEngine;

public class BallFallSound : MonoBehaviour
{
    private bool hasPlayedSound = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasPlayedSound && collision.relativeVelocity.magnitude > 1f)
        {
            hasPlayedSound = true;

        
            if (GameOverManager.Instance != null)
            {
                AudioSource audio = GameOverManager.Instance.GetComponent<AudioSource>();
                if (audio != null && GameOverManager.Instance.dropSound != null)
                {
                    audio.PlayOneShot(GameOverManager.Instance.dropSound);
                }
            }
        }
    }
}