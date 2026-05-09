using System.Diagnostics;
using UnityEngine;
public class LoseZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            // GameOverManager.Instance.TriggerGameOver();
        }
    }

}