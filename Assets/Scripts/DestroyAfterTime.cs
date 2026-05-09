using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifetime = 1.5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}