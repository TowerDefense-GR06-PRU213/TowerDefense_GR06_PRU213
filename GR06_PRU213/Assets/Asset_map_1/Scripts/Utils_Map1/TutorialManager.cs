using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Quản lý hệ thống Tutorial/Hướng dẫn chơi game
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private Image illustrationImage;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI pageIndicatorText;

    [Header("Tutorial Content")]
    [SerializeField] private List<TutorialPage> tutorialPages = new List<TutorialPage>();

    private int currentPageIndex = 0;

    private void Start()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }

        SetupButtons();
        SetupDefaultContent();
    }

    private void SetupButtons()
    {
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextPage);
        }

        if (prevButton != null)
        {
            prevButton.onClick.AddListener(PreviousPage);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseTutorial);
        }
    }

    private void SetupDefaultContent()
    {
        if (tutorialPages.Count == 0)
        {
            tutorialPages = new List<TutorialPage>
            {
                new TutorialPage
                {
                    title = "Welcome to Tower Defense!",
                    content = "Defend your base from waves of enemies!\n\n" +
                             "• Build towers to stop enemies\n" +
                             "• Upgrade your defenses\n" +
                             "• Survive all waves to win\n" +
                             "• Don't let enemies reach your gate!"
                },
                new TutorialPage
                {
                    title = "How to Play",
                    content = "CONTROLS:\n" +
                             "• Click to select platform\n" +
                             "• Choose hero to place\n" +
                             "• Earn gold by defeating enemies\n\n" +
                             "RESOURCES:\n" +
                             "• Gold: Buy/upgrade heroes\n" +
                             "• Lives: Don't let it reach 0!"
                },
                new TutorialPage
                {
                    title = "Enemy Types",
                    content = "MAP 1-3: Basic Enemies\n" +
                             "• Different HP and speeds\n\n" +
                             "MAP 4: Boss System\n" +
                             "• Powerful bosses\n\n" +
                             "MAP 5: Skilled Enemies\n" +
                             "• Enemies with special abilities\n" +
                             "• Hardest difficulty!"
                },
                new TutorialPage
                {
                    title = "Heroes & Abilities",
                    content = "HERO TYPES:\n\n" +
                             "• Melee: Close range, high damage\n" +
                             "• Ranged: Long range attacks\n" +
                             "• Support: Buffs and healing\n\n" +
                             "UPGRADES:\n" +
                             "• Increase damage\n" +
                             "• Faster attack speed"
                },
                new TutorialPage
                {
                    title = "Game Features",
                    content = "MAP 4 & 5 FEATURES:\n\n" +
                             "• Fast-Forward Button\n" +
                             "  Speed up gameplay\n\n" +
                             "• Resource Persistence\n" +
                             "  Progress carries over\n\n" +
                             "• 5 unique maps!"
                },
                new TutorialPage
                {
                    title = "Tips & Strategy",
                    content = "PRO TIPS:\n\n" +
                             "• Place heroes at path bends\n" +
                             "• Balance melee and ranged\n" +
                             "• Save gold for hard waves\n" +
                             "• Watch enemy paths\n\n" +
                             "Good luck!"
                }
            };
        }
    }

    public void OpenTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(true);
            currentPageIndex = 0;
            DisplayCurrentPage();
            
            // Bring panel to front
            tutorialPanel.transform.SetAsLastSibling();
        }
    }

    public void CloseTutorial()
    {
        if (tutorialPanel != null)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (currentPageIndex < tutorialPages.Count - 1)
        {
            currentPageIndex++;
            DisplayCurrentPage();
        }
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            DisplayCurrentPage();
        }
    }

    private void DisplayCurrentPage()
    {
        if (tutorialPages.Count == 0) return;

        TutorialPage currentPage = tutorialPages[currentPageIndex];

        if (titleText != null)
        {
            titleText.text = currentPage.title;
        }

        if (contentText != null)
        {
            contentText.text = currentPage.content;
        }

        if (illustrationImage != null && currentPage.illustration != null)
        {
            illustrationImage.sprite = currentPage.illustration;
            illustrationImage.gameObject.SetActive(true);
        }
        else if (illustrationImage != null)
        {
            illustrationImage.gameObject.SetActive(false);
        }

        if (pageIndicatorText != null)
        {
            pageIndicatorText.text = $"Page {currentPageIndex + 1} / {tutorialPages.Count}";
        }

        if (prevButton != null)
        {
            prevButton.interactable = currentPageIndex > 0;
        }

        if (nextButton != null)
        {
            nextButton.interactable = currentPageIndex < tutorialPages.Count - 1;
        }
    }
}

[System.Serializable]
public class TutorialPage
{
    public string title;
    [TextArea(5, 15)]
    public string content;
    public Sprite illustration;
}
