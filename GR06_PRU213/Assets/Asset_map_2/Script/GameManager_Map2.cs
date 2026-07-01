using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager for Map 2 - Manages game state, resources, and lives
/// Integrated with existing Map 2 systems (CastleHealth, GoldManager)
/// </summary>
public class GameManager_Map2 : MonoBehaviour
{
    public static GameManager_Map2 Instance { get; private set; }
    
    // Events for UI updates
    public static event Action<int> OnLivesChanged;
    public static event Action<int> OnResourcesChanged;
    public static event Action OnGameOver;
    public static event Action OnLevelComplete;

    [Header("Game State")]
    private int _lives = 5;
    private int _resources = 500;
    private int _currentWave = 0;
    private int _totalWaves = 5;
    
    [Header("References")]
    [SerializeField] private CastleHealth castleHealth;
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private EnemyWaveSpawner waveSpawner;

    // Public properties
    public int Resources => _resources;
    public int Lives => _lives;
    public int CurrentWave => _currentWave;
    public int TotalWaves => _totalWaves;

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

        // Initialize
        InitializeReferences();
    }

    private void OnEnable()
    {
        // Subscribe to events
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // Subscribe to Castle events if available
        if (castleHealth != null)
        {
            // castleHealth.OnCastleDestroyed += HandleCastleDestroyed;
            // castleHealth.OnCastleDamaged += HandleCastleDamaged;
        }

        // Subscribe to Wave events if available
        if (waveSpawner != null)
        {
            // waveSpawner.OnWaveStarted += HandleWaveStarted;
            // waveSpawner.OnWaveCompleted += HandleWaveCompleted;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
        if (castleHealth != null)
        {
            // castleHealth.OnCastleDestroyed -= HandleCastleDestroyed;
            // castleHealth.OnCastleDamaged -= HandleCastleDamaged;
        }

        if (waveSpawner != null)
        {
            // waveSpawner.OnWaveStarted -= HandleWaveStarted;
            // waveSpawner.OnWaveCompleted -= HandleWaveCompleted;
        }
    }

    private void Start()
    {
        ResetGameState();
        
        // Notify UI
        OnLivesChanged?.Invoke(_lives);
        OnResourcesChanged?.Invoke(_resources);
    }

    private void InitializeReferences()
    {
        // Auto-find references if not assigned
        if (castleHealth == null)
            castleHealth = FindObjectOfType<CastleHealth>();
        
        if (goldManager == null)
            goldManager = FindObjectOfType<GoldManager>();
        
        if (waveSpawner == null)
            waveSpawner = FindObjectOfType<EnemyWaveSpawner>();
    }

    #region Resource Management

    /// <summary>
    /// Add resources (gold) to player
    /// </summary>
    public void AddResources(int amount)
    {
        if (amount <= 0) return;
        
        _resources += amount;
        OnResourcesChanged?.Invoke(_resources);

        // Sync with GoldManager if exists
        if (goldManager != null)
        {
            // goldManager.AddGold(amount);
        }

        Debug.Log($"[GameManager_Map2] Resources added: {amount}. Total: {_resources}");
    }

    /// <summary>
    /// Spend resources - returns true if successful
    /// </summary>
    public bool SpendResources(int amount)
    {
        if (amount <= 0) return false;
        if (_resources < amount) return false;
        
        _resources -= amount;
        OnResourcesChanged?.Invoke(_resources);

        // Sync with GoldManager if exists
        if (goldManager != null)
        {
            // goldManager.SpendGold(amount);
        }

        Debug.Log($"[GameManager_Map2] Resources spent: {amount}. Remaining: {_resources}");
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

        Debug.Log($"[GameManager_Map2] Lives reduced by {damage}. Remaining: {_lives}");

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

        Debug.Log($"[GameManager_Map2] Lives added: {amount}. Total: {_lives}");
    }

    #endregion

    #region Wave Management

    /// <summary>
    /// Called when a wave starts
    /// </summary>
    public void HandleWaveStarted(int waveNumber)
    {
        _currentWave = waveNumber;
        Debug.Log($"[GameManager_Map2] Wave {waveNumber} started");
    }

    /// <summary>
    /// Called when a wave is completed
    /// </summary>
    public void HandleWaveCompleted(int waveNumber)
    {
        Debug.Log($"[GameManager_Map2] Wave {waveNumber} completed");

        // Check if all waves completed
        if (waveNumber >= _totalWaves)
        {
            HandleLevelComplete();
        }
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
            Debug.Log("[GameManager_Map2] Using default values (LevelManager not found)");
        }
        else
        {
            _lives = LevelManager.Instance.CurrentLevel.startingLives;
            _resources = LevelManager.Instance.CurrentLevel.startingResources;
            Debug.Log($"[GameManager_Map2] Loaded from LevelData - Lives: {_lives}, Resources: {_resources}");
        }

        _currentWave = 0;
        _isGameOver = false;
        _isPaused = false;

        // Notify UI
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
        Debug.Log("[GameManager_Map2] GAME OVER!");
        
        OnGameOver?.Invoke();
        
        // Optional: Pause game
        SetGameSpeed(0f);
    }

    /// <summary>
    /// Handle level complete state
    /// </summary>
    private void HandleLevelComplete()
    {
        if (_isGameOver) return;
        
        Debug.Log("[GameManager_Map2] LEVEL COMPLETE!");
        
        OnLevelComplete?.Invoke();
    }

    /// <summary>
    /// Handle castle destroyed (from CastleHealth)
    /// </summary>
    private void HandleCastleDestroyed()
    {
        HandleGameOver();
    }

    /// <summary>
    /// Handle castle damaged (from CastleHealth)
    /// </summary>
    private void HandleCastleDamaged(int currentHealth)
    {
        // Could update UI here
        Debug.Log($"[GameManager_Map2] Castle health: {currentHealth}");
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
        
        Debug.Log($"[GameManager_Map2] Game speed set to: {_gameSpeed}x");
    }

    /// <summary>
    /// Pause game
    /// </summary>
    public void PauseGame()
    {
        _isPaused = true;
        SetTimeScale(0f);
        Debug.Log("[GameManager_Map2] Game paused");
    }

    /// <summary>
    /// Resume game
    /// </summary>
    public void ResumeGame()
    {
        _isPaused = false;
        SetTimeScale(_gameSpeed);
        Debug.Log("[GameManager_Map2] Game resumed");
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

    #endregion

    #region Scene Management

    /// <summary>
    /// Restart current level
    /// </summary>
    public void RestartLevel()
    {
        Debug.Log("[GameManager_Map2] Restarting level...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Load next level
    /// </summary>
    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"[GameManager_Map2] Loading next level: Scene {nextSceneIndex}");
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
            Debug.Log("[GameManager_Map2] No more levels, returning to menu");
            ReturnToMenu();
        }
    }

    /// <summary>
    /// Return to main menu
    /// </summary>
    public void ReturnToMenu()
    {
        Debug.Log("[GameManager_Map2] Returning to main menu");
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

    #region Enemy Handling

    /// <summary>
    /// Called when an enemy is destroyed
    /// </summary>
    public void HandleEnemyDestroyed(int rewardAmount)
    {
        AddResources(rewardAmount);
    }

    /// <summary>
    /// Called when an enemy reaches the end
    /// </summary>
    public void HandleEnemyReachedEnd(int damage)
    {
        ReduceLives(damage);
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

    #endregion
}
