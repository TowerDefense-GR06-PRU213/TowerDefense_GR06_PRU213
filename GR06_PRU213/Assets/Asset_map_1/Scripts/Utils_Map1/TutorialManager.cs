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
            SetupBackgroundPanel();
            tutorialPanel.SetActive(false);
        }

        SetupButtons();
        SetupDefaultContent();
    }

    private void SetupBackgroundPanel()
    {
        if (tutorialPanel == null) return;

        // 1. Clean up nested Canvas and GraphicRaycaster components on TutorialPanel to prevent rendering bugs
        Canvas canvas = tutorialPanel.GetComponent<Canvas>();
        if (canvas != null) DestroyImmediate(canvas);

        GraphicRaycaster raycaster = tutorialPanel.GetComponent<GraphicRaycaster>();
        if (raycaster != null) DestroyImmediate(raycaster);

        // 2. Configure tutorialPanel itself to be a full-screen semi-transparent overlay
        // (dims the background and blocks clicks to elements behind)
        Image bgOverlayImage = tutorialPanel.GetComponent<Image>();
        if (bgOverlayImage == null) bgOverlayImage = tutorialPanel.AddComponent<Image>();
        if (bgOverlayImage != null)
        {
            bgOverlayImage.enabled = true;
            bgOverlayImage.color = new Color(0f, 0f, 0f, 0.65f); // 65% opacity black overlay to dim main menu
            bgOverlayImage.raycastTarget = true;
            
            // Assign a 1x1 white sprite to ensure solid color rendering
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();
            bgOverlayImage.sprite = Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
            bgOverlayImage.material = null;
        }

        RectTransform parentRT = tutorialPanel.GetComponent<RectTransform>();
        if (parentRT != null)
        {
            parentRT.anchorMin = Vector2.zero;
            parentRT.anchorMax = Vector2.one;
            parentRT.anchoredPosition = Vector2.zero;
            parentRT.sizeDelta = Vector2.zero;
            parentRT.localScale = Vector3.one;
        }

        // 3. Find or create the outer window panel (acting as a gold border, size: 910x710)
        Transform outerTransform = tutorialPanel.transform.Find("WindowOuter");
        GameObject windowOuter;
        if (outerTransform == null)
        {
            windowOuter = new GameObject("WindowOuter");
            windowOuter.transform.SetParent(tutorialPanel.transform, false);
        }
        else
        {
            windowOuter = outerTransform.gameObject;
        }

        RectTransform rtOuter = windowOuter.GetComponent<RectTransform>();
        if (rtOuter == null) rtOuter = windowOuter.AddComponent<RectTransform>();
        rtOuter.anchorMin = new Vector2(0.5f, 0.5f);
        rtOuter.anchorMax = new Vector2(0.5f, 0.5f);
        rtOuter.pivot = new Vector2(0.5f, 0.5f);
        rtOuter.anchoredPosition = Vector2.zero;
        rtOuter.sizeDelta = new Vector2(910, 710);
        rtOuter.localScale = Vector3.one;

        Image imgOuter = windowOuter.GetComponent<Image>();
        if (imgOuter == null) imgOuter = windowOuter.AddComponent<Image>();
        imgOuter.enabled = true;
        imgOuter.color = new Color(0.45f, 0.35f, 0.2f, 1f); // Gold/Brown border color (from image 2)
        Texture2D borderTex = new Texture2D(1, 1);
        borderTex.SetPixel(0, 0, Color.white);
        borderTex.Apply();
        imgOuter.sprite = Sprite.Create(borderTex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        imgOuter.material = null;

        // 4. Find or create the inner window panel (solid dark background, size: 900x700)
        Transform innerTransform = windowOuter.transform.Find("WindowInner");
        GameObject windowInner;
        if (innerTransform == null)
        {
            windowInner = new GameObject("WindowInner");
            windowInner.transform.SetParent(windowOuter.transform, false);
        }
        else
        {
            windowInner = innerTransform.gameObject;
        }

        RectTransform rtInner = windowInner.GetComponent<RectTransform>();
        if (rtInner == null) rtInner = windowInner.AddComponent<RectTransform>();
        rtInner.anchorMin = new Vector2(0.5f, 0.5f);
        rtInner.anchorMax = new Vector2(0.5f, 0.5f);
        rtInner.pivot = new Vector2(0.5f, 0.5f);
        rtInner.anchoredPosition = Vector2.zero;
        rtInner.sizeDelta = new Vector2(900, 700);
        rtInner.localScale = Vector3.one;

        Image imgInner = windowInner.GetComponent<Image>();
        if (imgInner == null) imgInner = windowInner.AddComponent<Image>();
        imgInner.enabled = true;
        imgInner.color = new Color(0.1f, 0.1f, 0.11f, 1f); // Opaque dark gray background (from image 2)
        Texture2D innerTex = new Texture2D(1, 1);
        innerTex.SetPixel(0, 0, Color.white);
        innerTex.Apply();
        imgInner.sprite = Sprite.Create(innerTex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        imgInner.material = null;

        // 5. Find the UI elements and reparent them to WindowInner
        Transform titleTextTrans = tutorialPanel.transform.Find("TitleText");
        Transform contentTextTrans = tutorialPanel.transform.Find("ContentText");
        Transform pageIndicatorTrans = tutorialPanel.transform.Find("PageIndicatorText");
        Transform nextBtnTrans = tutorialPanel.transform.Find("NextButton");
        Transform prevBtnTrans = tutorialPanel.transform.Find("PrevButton");
        Transform closeBtnTrans = tutorialPanel.transform.Find("CloseTutorialButton");

        if (titleTextTrans != null) titleTextTrans.SetParent(windowInner.transform, false);
        if (contentTextTrans != null) contentTextTrans.SetParent(windowInner.transform, false);
        if (pageIndicatorTrans != null) pageIndicatorTrans.SetParent(windowInner.transform, false);
        if (nextBtnTrans != null) nextBtnTrans.SetParent(windowInner.transform, false);
        if (prevBtnTrans != null) prevBtnTrans.SetParent(windowInner.transform, false);
        if (closeBtnTrans != null) closeBtnTrans.SetParent(windowInner.transform, false);

        // 6. Format and align elements within WindowInner
        if (titleTextTrans != null)
        {
            RectTransform rtTitle = titleTextTrans.GetComponent<RectTransform>();
            rtTitle.anchorMin = new Vector2(0.5f, 1f);
            rtTitle.anchorMax = new Vector2(0.5f, 1f);
            rtTitle.pivot = new Vector2(0.5f, 1f);
            rtTitle.anchoredPosition = new Vector2(0, -40);
            rtTitle.sizeDelta = new Vector2(800, 60);
        }
        if (contentTextTrans != null)
        {
            RectTransform rtContent = contentTextTrans.GetComponent<RectTransform>();
            rtContent.anchorMin = new Vector2(0.5f, 0.5f);
            rtContent.anchorMax = new Vector2(0.5f, 0.5f);
            rtContent.pivot = new Vector2(0.5f, 0.5f);
            rtContent.anchoredPosition = new Vector2(0, 15);
            rtContent.sizeDelta = new Vector2(800, 400);

            TextMeshProUGUI tmp = contentTextTrans.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.alignment = TextAlignmentOptions.TopLeft;
            }
        }
        if (pageIndicatorTrans != null)
        {
            RectTransform rtPage = pageIndicatorTrans.GetComponent<RectTransform>();
            rtPage.anchorMin = new Vector2(0.5f, 0f);
            rtPage.anchorMax = new Vector2(0.5f, 0f);
            rtPage.pivot = new Vector2(0.5f, 0f);
            rtPage.anchoredPosition = new Vector2(0, 45);
            rtPage.sizeDelta = new Vector2(200, 40);
            
            pageIndicatorText = pageIndicatorTrans.GetComponent<TextMeshProUGUI>();
        }
        if (prevBtnTrans != null)
        {
            RectTransform rtPrev = prevBtnTrans.GetComponent<RectTransform>();
            rtPrev.anchorMin = new Vector2(0f, 0f);
            rtPrev.anchorMax = new Vector2(0f, 0f);
            rtPrev.pivot = new Vector2(0f, 0f);
            rtPrev.anchoredPosition = new Vector2(40, 40);
            rtPrev.sizeDelta = new Vector2(160, 50);

            prevButton = prevBtnTrans.GetComponent<Button>();
        }
        if (nextBtnTrans != null)
        {
            RectTransform rtNext = nextBtnTrans.GetComponent<RectTransform>();
            rtNext.anchorMin = new Vector2(1f, 0f);
            rtNext.anchorMax = new Vector2(1f, 0f);
            rtNext.pivot = new Vector2(1f, 0f);
            rtNext.anchoredPosition = new Vector2(-40, 40);
            rtNext.sizeDelta = new Vector2(160, 50);

            nextButton = nextBtnTrans.GetComponent<Button>();
        }
        if (closeBtnTrans != null)
        {
            RectTransform rtClose = closeBtnTrans.GetComponent<RectTransform>();
            rtClose.anchorMin = new Vector2(1f, 1f);
            rtClose.anchorMax = new Vector2(1f, 1f);
            rtClose.pivot = new Vector2(1f, 1f);
            rtClose.anchoredPosition = new Vector2(-20, -20);
            rtClose.sizeDelta = new Vector2(50, 50);

            closeButton = closeBtnTrans.GetComponent<Button>();
        }

        Debug.Log("[TutorialManager] Setup window panel matching style 2 successfully!");
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
