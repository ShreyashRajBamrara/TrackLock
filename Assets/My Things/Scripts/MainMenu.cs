using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Scene build index references
    private const int LEVEL_MENU_INDEX = 1;
    private const int FIRST_LEVEL_INDEX = 2;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip backgroundMusicClip; 

    private AudioSource buttonAudioSource;
    private static AudioSource backgroundMusicSource; 

    void Awake()
    {
        buttonAudioSource = gameObject.AddComponent<AudioSource>();
        buttonAudioSource.playOnAwake = false;

        if (backgroundMusicSource == null)
        {
            GameObject musicObj = new GameObject("BackgroundMusic");
            DontDestroyOnLoad(musicObj);
            backgroundMusicSource = musicObj.AddComponent<AudioSource>();
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.playOnAwake = true;
            backgroundMusicSource.Play();
        }
    }

    public void StartGame()
    {
        PlayButtonClick();
        SceneManager.LoadScene(LEVEL_MENU_INDEX);
    }

    public void QuitGame()
    {
        PlayButtonClick();
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    private void PlayButtonClick()
    {
        if (buttonClickSound != null && buttonAudioSource != null)
        {
            buttonAudioSource.PlayOneShot(buttonClickSound);
        }
    }
}