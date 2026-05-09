using System.Collections.Generic;
using UnityEngine;

public class TriggerTimer : MonoBehaviour
{
    public string targetTag = "Ball"; 
    public float timeToTrigger = 3f;

    private Dictionary<GameObject, float> timerDict = new Dictionary<GameObject, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && !timerDict.ContainsKey(other.gameObject))
        {
            timerDict.Add(other.gameObject, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (timerDict.ContainsKey(other.gameObject))
        {
            timerDict.Remove(other.gameObject); 
        }
    }

    private void Update()
    {
        List<GameObject> keys = new List<GameObject>(timerDict.Keys);
        foreach (GameObject obj in keys)
        {
            if (obj == null) continue;

            timerDict[obj] += Time.deltaTime;

            if (timerDict[obj] >= timeToTrigger)
            {
                Debug.Log("Игра окончена: объект в триггере дольше 3 секунд");

                GameOverManager.Instance.ShowGameOverUI();
                
                timerDict.Clear();
                break;
            }
        }
    }
}