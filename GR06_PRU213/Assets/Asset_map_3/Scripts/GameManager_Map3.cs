using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager for Map 3 - Based on Map 1 structure
/// Manages game state, resources, and lives for Map 3
/// </summary>
public class GameManager_Map3 : MonoBehaviour
{
    public static GameManager_Map3 Instance { get; private set; }
    
    // Events for UI updates
    public static event Action<int> OnLivesChanged;
    public static event Action<int> OnResourcesChanged;
    public static event Action OnGameOver;
    public static event Action OnLevelComplete;

    [Header("Game State")]
    private int _lives = 5;
    private int _resources = 500;
    
    // Public properties
    public int Resources => _resources;
    public int Lives => _lives;

    private float _gameSpeed = 1f;
    public float GameSpeed => _gameSpeed;

    private bool _isGameOver = false;
    private bool _isPaused = false;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        // Subscribe to Enemy events (will be implemented)
        // Enemy_Map3.OnEnemyReachedEnd += HandleEnemyReachedEnd;
        // Enemy_Map3.OnEnemyDestroyed += HandleEnemyDestroyed;
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from Enemy events
        // Enemy_Map3.OnEnemyReachedEnd -= HandleEnemyReachedEnd;
        // Enemy_Map3.OnEnemyDestroyed -= HandleEnemyDestroyed;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        OnLivesChanged?.Invoke(_lives);
        OnResourcesChanged?.Invoke(_resources);
    }

    #region Resource Management

    /// <summary>
    /// Add resources to player
    /// </summary>
    public void AddResources(int amount)
    {
        if (amount <= 0) return;
        
        _resources += amount;
        OnResourcesChanged?.Invoke(_resources);
        
        Debug.Log($"[GameManager_Map3] Resources added: {amount}. Total: {_resources}");
    }

    /// <summary>
    /// Spend resources - returns true if successful
    /// </summary>
    public bool SpendResources(int amount)
    {
        if (amount <= 0) return false;
        if (_resources < amount)
        {
            Debug.LogWarning($"[GameManager_Map3] Not enough resources! Need: {amount}, Have: {_resources}");
            return false;
        }
        
        _resources -= amount;
        OnResourcesChanged?.Invoke(_resources);
        
        Debug.Log($"[GameManager_Map3] Resources spent: {amount}. Remaining: {_resources}");
        return true;
    }

    /// <summary>
    /// Check if player can afford something
    /// </summary>
    public bool CanAfford(int cost)
    {
        return _resources >= cost;
    }

    #endregion

    #region Lives Management

    /// <summary>
    /// Reduce lives when enemy reaches end
    /// </summary>
    public void ReduceLives(int damage)
    {
        if (_isGameOver) return;
        
        _lives = Mathf.Max(0, _lives - damage);
        OnLivesChanged?.Invoke(_lives);
        
        Debug.Log($"[GameManager_Map3] Lives reduced by {damage}. Remaining: {_lives}");

        if (_lives <= 0)
        {
            HandleGameOver();
        }
    }

    /// <summary>
    /// Add lives (for power-ups or rewards)
    /// </summary>
    public void AddLives(int amount)
    {
        if (amount <= 0) return;
        
        _lives += amount;
        OnLivesChanged?.Invoke(_lives);
        
        Debug.Log($"[GameManager_Map3] Lives added: {amount}. Total: {_lives}");
    }

    #endregion

    #region Enemy Handling

    /// <summary>
    /// Called when an enemy reaches the end
    /// </summary>
    private void HandleEnemyReachedEnd(object enemyData)
    {
        // Will be implemented with EnemyData_Map3
        // EnemyData_Map3 data = (EnemyData_Map3)enemyData;
        // ReduceLives(data.damage);
        
        // Placeholder
        ReduceLives(1);
    }

    /// <summary>
    /// Called when an enemy is destroyed
    /// </summary>
    private void HandleEnemyDestroyed(object enemy)
    {
        // Will be implemented with Enemy_Map3
        // Enemy_Map3 enemyComponent = (Enemy_Map3)enemy;
        // AddResources(Mathf.RoundToInt(enemyComponent.Data.resourceReward));
        
        // Placeholder
        AddResources(25);
    }

    #endregion

    #region Game State

    /// <summary>
    /// Reset game state to initial values
    /// </summary>
    public void ResetGameState()
    {
        // LAB 2: Use default values if LevelManager is null
        if (LevelManager.Instance == null || LevelManager.Instance.CurrentLevel == null)
        {
            _lives = 5;
            _resources = 500;
            Debug.Log("[GameManager_Map3] Using default values (LevelManager not found)");
        }
        else
        {
            _lives = LevelManager.Instance.CurrentLevel.startingLives;
            _resources = LevelManager.Instance.CurrentLevel.startingResources;
            Debug.Log($"[GameManager_Map3] Loaded from LevelData - Lives: {_lives}, Resources: {_resources}");
        }

        _isGameOver = false;
        _isPaused = false;

        OnLivesChanged?.Invoke(_lives);
        OnResourcesChanged?.Invoke(_resources);

        SetGameSpeed(1f);
    }

    /// <summary>
    /// Handle game over state
    /// </summary>
    private void HandleGameOver()
    {
        if (_isGameOver) return;
        
        _isGameOver = true;
        Debug.Log("[GameManager_Map3] GAME OVER!");
        
        OnGameOver?.Invoke();
        SetGameSpeed(0f);
    }

    /// <summary>
    /// Handle level complete state
    /// </summary>
    public void HandleLevelComplete()
    {
        if (_isGameOver) return;
        
        Debug.Log("[GameManager_Map3] LEVEL COMPLETE!");
        OnLevelComplete?.Invoke();
    }

    #endregion

    #region Game Speed Control

    /// <summary>
    /// Set game time scale
    /// </summary>
    public void SetTimeScale(float scale)
    {
        Time.timeScale = Mathf.Clamp(scale, 0f, 3f);
    }

    /// <summary>
    /// Set game speed (1x, 2x, 3x)
    /// </summary>
    public void SetGameSpeed(float newSpeed)
    {
        _gameSpeed = Mathf.Clamp(newSpeed, 0f, 3f);
        SetTimeScale(_gameSpeed);
        
        Debug.Log($"[GameManager_Map3] Game speed set to: {_gameSpeed}x");
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void PauseGame()
    {
        _isPaused = true;
        SetTimeScale(0f);
        Debug.Log("[GameManager_Map3] Game paused");
    }

    /// <summary>
    /// Resume game
    /// </summary>
    public void ResumeGame()
    {
        _isPaused = false;
        SetTimeScale(_gameSpeed);
        Debug.Log("[GameManager_Map3] Game resumed");
    }

    /// <summary>
    /// Toggle pause
    /// </summary>
    public void TogglePause()
    {
        if (_isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public bool IsPaused => _isPaused;

    #endregion

    #region Scene Management

    /// <summary>
    /// Restart current level
    /// </summary>
    public void RestartLevel()
    {
        Debug.Log("[GameManager_Map3] Restarting level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Load next level (or return to menu if last level)
    /// </summary>
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"[GameManager_Map3] Loading next level: Scene {nextSceneIndex}");
            Time.timeScale = 1f;
            
            // Update LevelManager if available
            if (LevelManager.Instance != null && 
                nextSceneIndex - 1 < LevelManager.Instance.allLevels.Length)
            {
                LevelManager.Instance.LoadLevel(LevelManager.Instance.allLevels[nextSceneIndex - 1]);
            }
            
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("[GameManager_Map3] Last level completed! Returning to menu");
            ReturnToMenu();
        }
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    public void ReturnToMenu()
    {
        Debug.Log("[GameManager_Map3] Returning to main menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // MainMenu is scene 0
    }

    /// <summary>
    /// Called when scene is loaded
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameState();
    }

    #endregion

    #region Debug

    /// <summary>
    /// Add resources cheat (for testing)
    /// </summary>
    [ContextMenu("Debug: Add 1000 Resources")]
    private void DebugAddResources()
    {
        AddResources(1000);
    }

    /// <summary>
    /// Complete level cheat (for testing)
    /// </summary>
    [ContextMenu("Debug: Complete Level")]
    private void DebugCompleteLevel()
    {
        HandleLevelComplete();
    }

    /// <summary>
    /// Game Over cheat (for testing)
    /// </summary>
    [ContextMenu("Debug: Trigger Game Over")]
    private void DebugGameOver()
    {
        _lives = 0;
        HandleGameOver();
    }

    #endregion
}
