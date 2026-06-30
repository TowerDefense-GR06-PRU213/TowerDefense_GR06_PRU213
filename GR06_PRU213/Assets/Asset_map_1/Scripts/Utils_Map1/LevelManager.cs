using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelData[] allLevels;
    public LevelData CurrentLevel { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        // LAB 2 FIX: Add null check for allLevels array
        if (allLevels == null || allLevels.Length == 0)
        {
            Debug.LogError("[LevelManager] allLevels array is null or empty! Please assign LevelData in Inspector.");
            CurrentLevel = null;
        }
        else
        {
            CurrentLevel = allLevels[0];
            Debug.Log($"[LevelManager] Initialized with {allLevels.Length} levels. Current: {CurrentLevel.levelName}");
        }
    }

    public void LoadLevel(LevelData levelData)
    {
        // LAB 2 FIX: Add null check
        if (levelData == null)
        {
            Debug.LogError("[LevelManager] Cannot load level - levelData is null!");
            return;
        }
        
        CurrentLevel = levelData;
        Debug.Log($"[LevelManager] Loading level: {levelData.levelName}");
        SceneManager.LoadScene(levelData.levelName);
    }
}