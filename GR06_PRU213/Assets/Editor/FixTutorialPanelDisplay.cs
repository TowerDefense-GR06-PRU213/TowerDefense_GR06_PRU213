using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Reflection;

public class FixTutorialPanelDisplay
{
    [MenuItem("Tools/Fix Tutorial Panel Display 🎨")]
    public static void FixDisplay()
    {
        // Find Tutorial Panel (even if inactive)
        GameObject tutorialPanel = FindGameObjectInActiveScene("TutorialPanel");
        if (tutorialPanel == null)
        {
            EditorUtility.DisplayDialog("Error", "TutorialPanel not found in current scene! Ensure you are in MainMenu scene.", "OK");
            return;
        }

        // Remove Canvas component if it exists to avoid nested WorldSpace Canvas rendering issues
        Canvas canvas = tutorialPanel.GetComponent<Canvas>();
        if (canvas != null)
        {
            Object.DestroyImmediate(canvas);
            Debug.Log("✅ Removed Canvas component from TutorialPanel to fix rendering");
        }

        // Remove GraphicRaycaster component if it exists (not needed since parent canvas handles raycasting)
        GraphicRaycaster raycaster = tutorialPanel.GetComponent<GraphicRaycaster>();
        if (raycaster != null)
        {
            Object.DestroyImmediate(raycaster);
            Debug.Log("✅ Removed GraphicRaycaster from TutorialPanel");
        }

        // Fix 1: Make background dark translucent black for rich aesthetics
        Image panelImage = tutorialPanel.GetComponent<Image>();
        if (panelImage == null)
        {
            panelImage = tutorialPanel.AddComponent<Image>();
        }
        if (panelImage != null)
        {
            panelImage.color = new Color(0f, 0f, 0f, 0.92f); // 92% opaque black overlay
            panelImage.raycastTarget = true; // Block clicks to background elements
            Debug.Log("✅ TutorialPanel background set to dark translucent black (92% opacity)");
        }

        // Fix 2: Auto-link pageIndicatorText in TutorialManager
        TutorialManager tutorialManager = FindComponentInActiveScene<TutorialManager>();
        bool linkedPageText = false;
        if (tutorialManager != null)
        {
            Transform pageIndicator = tutorialPanel.transform.Find("PageIndicatorText");
            if (pageIndicator != null)
            {
                TextMeshProUGUI tmpText = pageIndicator.GetComponent<TextMeshProUGUI>();
                if (tmpText != null)
                {
                    FieldInfo field = typeof(TutorialManager).GetField("pageIndicatorText", 
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(tutorialManager, tmpText);
                        linkedPageText = true;
                        Debug.Log("✅ Automatically linked PageIndicatorText to TutorialManager");
                    }
                }
            }
        }

        // Fix 3: Also fix Level Selection Panel if exists (remove Canvas/GraphicRaycaster, set background)
        GameObject levelPanel = FindGameObjectInActiveScene("LevelSelectionPanel");
        if (levelPanel != null)
        {
            Canvas levelCanvas = levelPanel.GetComponent<Canvas>();
            if (levelCanvas != null)
            {
                Object.DestroyImmediate(levelCanvas);
                Debug.Log("✅ Removed Canvas component from LevelSelectionPanel");
            }

            GraphicRaycaster levelRaycaster = levelPanel.GetComponent<GraphicRaycaster>();
            if (levelRaycaster != null)
            {
                Object.DestroyImmediate(levelRaycaster);
                Debug.Log("✅ Removed GraphicRaycaster from LevelSelectionPanel");
            }

            Image levelImage = levelPanel.GetComponent<Image>();
            if (levelImage == null)
            {
                levelImage = levelPanel.AddComponent<Image>();
            }
            if (levelImage != null)
            {
                levelImage.color = new Color(0f, 0f, 0f, 0.92f);
                levelImage.raycastTarget = true;
            }
            
            Debug.Log("✅ Level Selection Panel background and click blocking fixed");

            // Auto-link levelSelectionPanel in MainMenuController
            MainMenuController mainMenuController = FindComponentInActiveScene<MainMenuController>();
            if (mainMenuController != null)
            {
                FieldInfo field = typeof(MainMenuController).GetField("levelSelectionPanel", 
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(mainMenuController, levelPanel);
                    Debug.Log("✅ Automatically linked LevelSelectionPanel to MainMenuController");
                }
            }
        }

        // Mark scene dirty to save changes
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        Debug.Log("✅ ALL FIXED!");
        EditorUtility.DisplayDialog("Success! 🎉", 
            "MainMenu Tutorial Panel and connections fixed!\n\n" +
            "✅ Background set to dark translucent black (92% opacity)\n" +
            "✅ Nested Canvas components removed to allow correct Screen Space rendering\n" +
            (linkedPageText ? "✅ Page Indicator text linked successfully!\n" : "⚠️ Page Indicator text was NOT linked (check object hierarchy)\n") +
            "✅ Level Selection Panel registered if present\n\n" +
            "Please save the scene and play to test!", 
            "Awesome!");
    }

    // Helper method to find any GameObject (active or inactive) in the current active scene
    private static GameObject FindGameObjectInActiveScene(string name)
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        var rootObjects = scene.GetRootGameObjects();
        foreach (var root in rootObjects)
        {
            GameObject result = FindChildRecursive(root, name);
            if (result != null) return result;
        }
        return null;
    }

    private static GameObject FindChildRecursive(GameObject current, string name)
    {
        if (current.name == name) return current;
        foreach (Transform child in current.transform)
        {
            GameObject result = FindChildRecursive(child.gameObject, name);
            if (result != null) return result;
        }
        return null;
    }

    // Helper method to find a component in the scene (even if on an inactive GameObject)
    private static T FindComponentInActiveScene<T>() where T : Component
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        var rootObjects = scene.GetRootGameObjects();
        foreach (var root in rootObjects)
        {
            T comp = root.GetComponentInChildren<T>(true);
            if (comp != null) return comp;
        }
        return null;
    }
}
