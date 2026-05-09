using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI.Extensions;

[Serializable]
public class SavedCatData
{
    public int level;
    public Vector3 position;
}

[Serializable]
public class CatsSaveData
{
    public List<SavedCatData> cats = new List<SavedCatData>();
}


public class GameManager : MonoBehaviour
{
    public GameObject catsPrefab;
    public Transform spawnPoint;
    public Sprite[] spritesByLevel;
    public Transform leftX;
    public Transform rightX;
    public GameObject mergeEffectPrefab;
    private AudioSource audioSource;
    private GameObject currentCats;
    public Transform catsContainer;
    public TrajectoryLine trajectoryLine;
    public RectTransform spawnPointUI; 

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        LoadCats();
        StartCoroutine(SaveTimer());
    }

    private void Update()
    {
        if (EventSystem.current != null)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Time.timeScale < 1f)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
            {
                if (currentCats == null)
                {
                    SpawnCats();
                }
            }
        if (Input.GetMouseButton(0) && currentCats != null)
        {
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = Input.mousePosition;

            float x = Mathf.Clamp(pos.x, leftX.position.x, rightX.position.x);
            Vector3 clampPos = new Vector3(x, leftX.position.y, 0);
            currentCats.transform.position = clampPos;
            //trajectoryLine.UpdateLine();
        }

        if (Input.GetMouseButtonUp(0) && currentCats != null)
        {
            currentCats.GetComponent<Rigidbody2D>().simulated = true;
            trajectoryLine.StopAiming();
            currentCats = null;
        }

    }
    private IEnumerator SaveTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            SaveCats();
        }
    }
    public void SpawnCats()
    {
        GameObject go = Instantiate(catsPrefab, catsContainer);
        Cats cat = go.GetComponent<Cats>();
        cat.level = UnityEngine.Random.Range(1, 4);
        cat.levelSprites = spritesByLevel;
        cat.gameManager = this;
        currentCats = go;
        currentCats.GetComponent<Rigidbody2D>().simulated = false;
        RectTransform rt = currentCats.GetComponent<RectTransform>();
        trajectoryLine.dynamicStartPoint = rt;
        trajectoryLine.StartAiming();
    }

    public void Merge(Cats v1, Cats v2)
    {
        Vector3 pos = v1.transform.position;

        GameObject newCat = Instantiate(catsPrefab, new Vector3(pos.x, pos.y, catsContainer.position.z), Quaternion.identity, catsContainer);
        Cats cat = newCat.GetComponent<Cats>();
        cat.levelSprites = spritesByLevel;
        cat.level = v1.level + 1;
        cat.gameManager = this;

        // if (mergeEffectPrefab != null)
        // {
        //     Vector3 effectPos = new Vector3(pos.x, pos.y, catsContainer.position.z);
        //     GameObject effect = Instantiate(mergeEffectPrefab, effectPos, Quaternion.identity, catsContainer);
        //     //Destroy(effect, 2f);
        // }

        int mergeScore = 100;
        ScoreManager.Instance.AddScore(mergeScore);

        if (GameOverManager.Instance != null)
        {
            AudioSource audio = GameOverManager.Instance.GetComponent<AudioSource>();
            if (audio != null && GameOverManager.Instance.mergeSound != null)
            {
                audio.PlayOneShot(GameOverManager.Instance.GetRandomMergeSound());
            }
        }

        Destroy(v1.gameObject);
        Destroy(v2.gameObject);

        Rigidbody2D rb = newCat.GetComponent<Rigidbody2D>();
        rb.simulated = true;
    }
    public void SaveCats()
    {
        CatsSaveData saveData = new CatsSaveData();

        foreach (Transform child in catsContainer)
        {
            Cats cat = child.GetComponent<Cats>();
            if (cat != null)
            {
                SavedCatData data = new SavedCatData
                {
                    level = cat.level,
                    position = cat.transform.position
                };

                saveData.cats.Add(data);
            }
        }

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SavedCats", json);
        PlayerPrefs.Save();
    }
    public void LoadCats()
    {
        if (!PlayerPrefs.HasKey("SavedCats")) return;

        string json = PlayerPrefs.GetString("SavedCats");
        CatsSaveData saveData = JsonUtility.FromJson<CatsSaveData>(json);

        foreach (SavedCatData catData in saveData.cats)
        {
            GameObject go = Instantiate(catsPrefab, catData.position, Quaternion.identity, catsContainer);
            Cats cat = go.GetComponent<Cats>();
            cat.level = catData.level;
            cat.levelSprites = spritesByLevel;
            cat.gameManager = this;
        }
    }

}

public class TrajectoryUILine
{
    internal void StartAiming()
    {
        throw new NotImplementedException();
    }

    internal void StopAiming()
    {
        throw new NotImplementedException();
    }
}