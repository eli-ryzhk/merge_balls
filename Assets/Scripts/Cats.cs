//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Cats : MonoBehaviour
{
    public int level = 1;
    public float scaleFactor = 0.1f;
    public Sprite[] levelSprites; 
    public GameManager gameManager;
    public bool isMerget;

    private void Start()
    {
        UpdateSprite();
    }

    public void Upgrade()
    {
        level++;
        if (level >= levelSprites.Length) {
            level = levelSprites.Length - 1;
        }
        Debug.Log(transform.position);
        UpdateSprite();
    }

    private void UpdateSprite()
    {

        GetComponent<SpriteRenderer>().sprite = levelSprites[level - 1];
        GetComponent<UnityEngine.UI.Image>().sprite = levelSprites[level - 1];
        transform.localScale = Vector3.one + (Vector3.one * (level-1) * scaleFactor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cats other = collision.gameObject.GetComponent<Cats>();
        if (other != null && other.level == level)
        {
            if (other.isMerget == true)
            {
                return;
            }
            gameManager.Merge(this, other);
            isMerget = true;
            other.isMerget = true;
        }
    }
}