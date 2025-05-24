using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public string[] levelNames; // Add Level 1 Time, level 2, level 3 in Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            LoadLevelOne();
        }
    }

    void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int currentIndex = System.Array.IndexOf(levelNames, currentScene);
        int nextIndex = (currentIndex + 1) % levelNames.Length;

        SceneManager.LoadScene(levelNames[nextIndex]);
    }

    void LoadLevelOne()
    {
        if (levelNames.Length > 0)
        {
            SceneManager.LoadScene(levelNames[0]); // Always loads Level 1
        }
    }
}
