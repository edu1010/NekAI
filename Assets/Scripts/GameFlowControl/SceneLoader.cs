using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] LevelNames;

    public string LoadingSceneName;

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }

    public void LoadLevel(int level)
    {
        LoadingSceneName = LevelNames[level];

        SceneManager.LoadScene("Loading");
        LoadSceneAsync(LoadingSceneName);
    } 
    public void LoadLevelWithName(string levelToGo)
    {
        LoadingSceneName = levelToGo;

        SceneManager.LoadScene("Loading");
        LoadSceneAsync(LoadingSceneName);
    }


    private void LoadSceneAsync(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }
}