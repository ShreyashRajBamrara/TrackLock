using UnityEngine;
#if TMP_PRESENT
using TMPro; // Only use if TextMeshPro is installed
#endif

public class TrainCollision : MonoBehaviour 
{
    [Header("UI Settings")]
    [SerializeField] GameObject gameOverCanvas;
    
    [Header("Audio Settings")]
    [SerializeField] AudioClip crashSound; // New field for crash sound

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obst"))   
        {
            PlayCrashSound(); // Play sound before destruction
            Destroy(gameObject);
            ShowCrashUI();
        }
    }

    void ShowCrashUI()
    {
        Time.timeScale = 0f;
        
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    void PlayCrashSound()
    {
        if (crashSound != null)
        {
            
            GameObject audioObject = new GameObject("CrashSound");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(crashSound);
            Destroy(audioObject, crashSound.length); 
        }
    }
}