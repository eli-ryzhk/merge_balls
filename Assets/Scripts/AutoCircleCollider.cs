using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(Image))]
//[RequireComponent(typeof(CircleCollider2D))]
public class AutoCircleCollider : MonoBehaviour
{
    void Start()
    {
        UpdateCircleCollider();
    }

    public void UpdateCircleCollider()
    {
        Image sr = GetComponent<Image>();
        CircleCollider2D cc = GetComponent<CircleCollider2D>();

        if (sr.sprite == null) return;
        float spriteRadius = Mathf.Max(sr.sprite.bounds.extents.x, sr.sprite.bounds.extents.y);


        float maxScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y);
        cc.radius = spriteRadius * maxScale;
        cc.offset = sr.sprite.bounds.center;
    }
}