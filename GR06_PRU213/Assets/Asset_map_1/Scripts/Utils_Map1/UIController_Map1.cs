using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIController_Map1 : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text resourcesText;
    [SerializeField] private TMP_Text warningText;

    [SerializeField] private GameObject HeroPanel;
    [SerializeField] private GameObject HeroCardPrefab;
    [SerializeField] private Transform cardsContainer;

    [SerializeField] private HeroData_Map1[] hero;
    private List<GameObject> activeCards = new List<GameObject>();

    private Platform_Map1 _currentPlatform;

    [SerializeField] private Button speedButton;
    [SerializeField] private TMP_Text speedButtonText; // Hiển thị "x1", "x2", "x3" (có thể để trống nếu không cần)
    [SerializeField] private Button muteButton;

    [SerializeField] private Color normalButtonColor = Color.white;
    [SerializeField] private Color selectedButtonColor = Color.blue;
    [SerializeField] private Color normalTextColor = Color.black;
    [SerializeField] private Color selectedTextColor = Color.white;

    private readonly float[] speedLevels = { 1f, 2f, 3f };
    private int currentSpeedIndex = 0;

    [SerializeField] private GameObject pausePanel;
    private bool _isGamePaused = false;
    private bool _isMuted = false;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private GameObject missionCompletePanel;

    private void OnEnable()
    {
        Spawner_Map1.OnWaveChanged += UpdateWaveText;
        GameManager_Map1.OnLivesChanged += UpdateLivesText;
        GameManager_Map1.OnResourcesChanged += UpdateResourcesText;
        Platform_Map1.OnPlatformClicked += HandlePlatformClicked;
        HeroCard_Map1.OnTowerSelected += HandleHeroSelected;
        SceneManager.sceneLoaded += OnSceneLoaded;
        Spawner_Map1.OnMissionComplete += ShowMissionComplete;
    }

    private void OnDisable()
    {
        Spawner_Map1.OnWaveChanged -= UpdateWaveText;
        GameManager_Map1.OnLivesChanged -= UpdateLivesText;
        GameManager_Map1.OnResourcesChanged -= UpdateResourcesText;
        Platform_Map1.OnPlatformClicked -= HandlePlatformClicked;
        HeroCard_Map1.OnTowerSelected -= HandleHeroSelected;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Spawner_Map1.OnMissionComplete -= ShowMissionComplete;
    }

    private void Start()
    {
        speedButton.onClick.AddListener(CycleGameSpeed);

        // Khởi tạo tốc độ ban đầu dựa trên GameSpeed hiện tại (nếu có), mặc định x1
        currentSpeedIndex = Array.IndexOf(speedLevels, GameManager_Map1.Instance.GameSpeed);
        if (currentSpeedIndex < 0) currentSpeedIndex = 0;
        UpdateSpeedButtonVisual();

        _isMuted = false; 
        AudioListener.volume = 1f; 
        UpdateButtonVisual(muteButton, _isMuted); 
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }


    private void UpdateWaveText(int currentWave)
    {
        waveText.text = $"Wave: {currentWave + 1}";
    }

    private void UpdateLivesText(int currentLives)
    {
        livesText.text = $"{currentLives}";
        if (currentLives <= 0)
        {
            ShowGameOver();
        }

    }
    private void UpdateResourcesText(int currentResources)
    {
        resourcesText.text = $"{currentResources}";
    }


    private void HandlePlatformClicked(Platform_Map1 platform)
    {
        _currentPlatform = platform;
        ShowHeroPanel();
    }

    private void ShowHeroPanel()
    {
        HeroPanel.SetActive(true);
        Platform_Map1.heroPanelOpen = true;
        GameManager_Map1.Instance.SetTimeScale(0f);
        PopulateHeroCards();
    }

    public void HideHeroPanel()
    {
        HeroPanel.SetActive(false);
        Platform_Map1.heroPanelOpen = false;
        GameManager_Map1.Instance.SetTimeScale(GameManager_Map1.Instance.GameSpeed);

    }

    private void PopulateHeroCards()
    {
        foreach (var card in activeCards)
        {
            Destroy(card);
        }
        activeCards.Clear();

        foreach (var data in hero)
        {
            GameObject cardGameObject = Instantiate(HeroCardPrefab, cardsContainer);
            HeroCard_Map1 card = cardGameObject.GetComponent<HeroCard_Map1>();
            card.Initialize(data);
            activeCards.Add(cardGameObject);
        }
    }

    private void HandleHeroSelected(HeroData_Map1 heroData)
    {
        if (_currentPlatform.transform.childCount > 0)
        {
            HideHeroPanel();
            StartCoroutine(ShowWarningMessage("This platform already has a Hero!"));
            return;
        }

        if (GameManager_Map1.Instance.Resources >= heroData.cost)
        {
            GameManager_Map1.Instance.SpendResources(heroData.cost);
            _currentPlatform.PlaceHero(heroData);
        }
        else
        {
            StartCoroutine(ShowWarningMessage("Not Enough Resources"));
        }

        HideHeroPanel();

    }

    private IEnumerator ShowWarningMessage(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        warningText.gameObject.SetActive(false);
    }

    private void CycleGameSpeed()
    {
        currentSpeedIndex = (currentSpeedIndex + 1) % speedLevels.Length;
        float newSpeed = speedLevels[currentSpeedIndex];

        GameManager_Map1.Instance.SetGameSpeed(newSpeed);
        UpdateSpeedButtonVisual();
    }

    private void UpdateSpeedButtonVisual()
    {
        float speed = speedLevels[currentSpeedIndex];

        if (speedButtonText != null)
        {
            speedButtonText.text = $"x{speed:0.#}";
        }

        // Đổi màu nút khi đang ở mức tốc độ cao nhất (x3), có thể bỏ dòng dưới nếu không cần
        UpdateButtonVisual(speedButton, currentSpeedIndex == speedLevels.Length - 1);
    }

    private void UpdateButtonVisual(Button button, bool isSelected)
    {
        button.image.color = isSelected ? selectedButtonColor : normalButtonColor;

        TMP_Text text = button.GetComponentInChildren<TMP_Text>();
        if (text != null)
        {
            text.color = isSelected ? selectedTextColor : normalTextColor;
        }
    }


    public void TogglePause()
    {
        if (HeroPanel.activeSelf)
            return;

        if (_isGamePaused)
        {
            pausePanel.SetActive(false);
            _isGamePaused = false;
            GameManager_Map1.Instance.SetTimeScale(GameManager_Map1.Instance.GameSpeed);
        }
        else
        {
            pausePanel.SetActive(true);
            _isGamePaused = true;
            GameManager_Map1.Instance.SetTimeScale(0f);
        }
    }


    public void ToggleMute()
    {
        // 1. Đảo ngược trạng thái (đang tắt -> bật, đang bật -> tắt)
        _isMuted = !_isMuted;

        // 2. Cập nhật âm lượng tổng của game
        if (_isMuted)
        {
            AudioListener.volume = 0f; // Tắt tiếng
        }
        else
        {
            AudioListener.volume = 1f; // Bật tiếng
        }

        // 3. Cập nhật màu sắc của nút
        // Chúng ta tận dụng hàm UpdateButtonVisual bạn đã có.
        // Khi _isMuted = true (đã tắt), nút sẽ có màu "selected" (màu xanh)
        UpdateButtonVisual(muteButton, _isMuted);
    }

    public void RestartLevel()
    {
        // CRITICAL: Reset time scale BEFORE reloading scene
        Time.timeScale = 1f;
        GameManager_Map1.Instance.SetGameSpeed(1f);
        
        // Hide game over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        
        // Load current level again
        if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevel != null)
        {
            LevelManager.Instance.LoadLevel(LevelManager.Instance.CurrentLevel);
        }
        else
        {
            // Fallback: Reload current scene directly
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void GoToMainMenu()
    {
        // Reset time scale before leaving
        Time.timeScale = 1f;
        if (GameManager_Map1.Instance != null)
            GameManager_Map1.Instance.SetGameSpeed(1f);
        
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowGameOver()
    {
        // Stop the game
        Time.timeScale = 0f;
        if (GameManager_Map1.Instance != null)
            GameManager_Map1.Instance.SetTimeScale(0f);
        
        // Hide pause panel if it's open
        if (pausePanel != null && pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            _isGamePaused = false;
        }
        
        // Show game over panel
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ShowObjective());
    }
    private IEnumerator ShowObjective()
    {
        // LAB 2 FIX: Add null checks
        if (objectiveText == null)
        {
            Debug.LogWarning("[UIController_Map1] objectiveText is not assigned!");
            yield break;
        }

        if (LevelManager.Instance == null || LevelManager.Instance.CurrentLevel == null)
        {
            Debug.LogWarning("[UIController_Map1] LevelManager or CurrentLevel is null!");
            objectiveText.text = "Survive the waves!";
        }
        else
        {
            objectiveText.text = $"Survive {LevelManager.Instance.CurrentLevel.wavesToWin} waves!";
        }
        
        objectiveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        objectiveText.gameObject.SetActive(false);
    }

    private void ShowMissionComplete()
    {
        missionCompletePanel.SetActive(true);
        GameManager_Map1.Instance.SetTimeScale(0f);
    }

    public void EnterEndlessMode()
    {
        missionCompletePanel.SetActive(false);
        GameManager_Map1.Instance.SetTimeScale(GameManager_Map1.Instance.GameSpeed);
        Spawner_Map1.Instance.EnableEndlessMode();
    }

    public void LoadNextLevel()
    {
        // Fix: Load trực tiếp Map 2 sau khi thắng Map 1
        GameManager_Map1.Instance.SetTimeScale(1f); // Reset time scale
        missionCompletePanel.SetActive(false);

        PlayerData.Instance.gold = GameManager_Map1.Instance.Resources;
        PlayerData.Instance.lives = GameManager_Map1.Instance.Lives;
        // Thử load qua LevelManager trước
        var levelManager = LevelManager.Instance;
        if (levelManager != null && levelManager.allLevels != null && levelManager.allLevels.Length > 0)
        {
            int currentIndex = System.Array.IndexOf(levelManager.allLevels, levelManager.CurrentLevel);
            int nextIndex = currentIndex + 1;
            if (nextIndex < levelManager.allLevels.Length)
            {
                levelManager.LoadLevel(levelManager.allLevels[nextIndex]);
                return;
            }
        }
        
        // Fallback: Load trực tiếp Map 2 nếu LevelManager không hoạt động
        Debug.Log("[UIController_Map1] Loading Game_Map2 directly");
        SceneManager.LoadScene("Game_Map2");
    }


}