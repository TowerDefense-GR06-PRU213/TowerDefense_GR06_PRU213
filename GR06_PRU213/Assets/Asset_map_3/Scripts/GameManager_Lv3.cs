using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Lv3 : MonoBehaviour
{
    public static GameManager_Lv3 Instance { get; private set; }

    public static event Action<int> OnLivesChanged;

    private int _lives = 5;
    public int CurrentLives => _lives;   // thêm getter

    private float _gameSpeed = 1f;
    public float GameSpeed => _gameSpeed;

    [Header("UI Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Enemy_Lv3.OnEnemyReachedEnd += HanldeEnemyReachesEnd;
        Spawner_Lv3.OnAllWavesCompleted += HandleAllWavesCompleted;
    }

    private void OnDisable()
    {
        Enemy_Lv3.OnEnemyReachedEnd -= HanldeEnemyReachesEnd;
        Spawner_Lv3.OnAllWavesCompleted -= HandleAllWavesCompleted;
    }

    private IEnumerator Start()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (PlayerData.Instance != null)
        {
            _lives = PlayerData.Instance.lives;
            Debug.Log("Da lay Lives tu PlayerData = " + _lives);
        }
        else if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
        {
            _lives = LevelManager.Instance.CurrentLevel.startingLives;
            Debug.Log("Lay lives tu LevelManager = " + _lives);
        }
        else
        {
            _lives = 5;
        }

        yield return null;   // đợi UI load xong

        Debug.Log("GameManager gửi lives = " + _lives);
        OnLivesChanged?.Invoke(_lives);

        Time.timeScale = 1f;
    }

    private void HanldeEnemyReachesEnd(EnemyData_Lv3 data)
    {
        _lives = Mathf.Max(0, _lives - data.damage);

        Debug.Log("Lives còn = " + _lives);
        OnLivesChanged?.Invoke(_lives);

        if (_lives <= 0)
        {
            Time.timeScale = 0f;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }
    }

    private void HandleAllWavesCompleted()
    {
        if (_lives > 0)
        {
            Time.timeScale = 0f;

            if (victoryPanel != null)
                victoryPanel.SetActive(true);

            Debug.Log("CHIẾN THẮNG");
        }
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void SetGameSpeed(float newSpeed)
    {
        _gameSpeed = newSpeed;
        SetTimeScale(_gameSpeed);
    }
}