using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Button musicOnButton;
    public Button musicOffButton;

    private bool isMusicOn = true;
    private AudioSource[] musicSources;

    private void Start()
    {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        musicSources = new AudioSource[musicObjects.Length];

        for (int i = 0; i < musicObjects.Length; i++)
        {
            musicSources[i] = musicObjects[i].GetComponent<AudioSource>();
        }

        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        ApplyMusicState();

        musicOnButton.onClick.AddListener(() => SetMusicState(false));
        musicOffButton.onClick.AddListener(() => SetMusicState(true));
    }

    private void SetMusicState(bool turnOn)
    {
        isMusicOn = turnOn;
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMusicState();
    }

    private void ApplyMusicState()
    {
        // foreach (AudioSource source in musicSources)
        // {
        //    if (source != null)
        //        source.mute = !isMusicOn;
        // }

        musicOnButton.gameObject.SetActive(isMusicOn);
        musicOffButton.gameObject.SetActive(!isMusicOn);
        AudioListener.pause = !isMusicOn;
    }
}
