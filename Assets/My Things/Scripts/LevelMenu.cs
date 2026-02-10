using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // Simple functions for each level button
    public void LoadLevel1() => LoadLevel("Level 1");
    public void LoadLevel2() => LoadLevel("Level 2"); 
    public void LoadLevel3() => LoadLevel("Level 3");
    public void LoadLevel4() => LoadLevel("Level 4");
    public void LoadLevel5() => LoadLevel("Level 5");
    public void LoadMenu() => LoadLevel("Main Menu");

    private void LoadLevel(string sceneName)
    {
        Time.timeScale = 1f; // Reset pause state
        SceneManager.LoadScene(sceneName);
    }

    // Optional: Add debug to verify scene exists
    private void OnValidate()
    {
        #if UNITY_EDITOR
        Debug.Log($"Level 1 exists: {SceneExists("Level 1")}");
        Debug.Log($"Level 5 exists: {SceneExists("Level 5")}");
        #endif
    }

    private bool SceneExists(string sceneName)
    {
        return SceneUtility.GetBuildIndexByScenePath(sceneName) >= 0;
    }
}