using System.Collections.Generic;
using UnityEngine;



public class SpriteScaler : MonoBehaviour
{
    //[Header("Список объектов и их масштабов")]
    [System.Serializable]
    public class SpriteScalePair
    {
        public GameObject spriteObject;
        public Vector3 scale;
    }
    public List<SpriteScalePair> spriteScalePairs;

    private void Start()
    {
        foreach (var pair in spriteScalePairs)
        {
            if (pair.spriteObject != null)
            {
                pair.spriteObject.transform.localScale = pair.scale;
            }
        }
    }
}