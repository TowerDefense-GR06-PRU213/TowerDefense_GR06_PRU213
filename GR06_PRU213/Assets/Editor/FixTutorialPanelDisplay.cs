using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FixTutorialPanelDisplay
{
    [MenuItem("Tools/Fix Tutorial Panel Display 🎨")]
    public static void FixDisplay()
    {
        // Find Tutorial Panel
        GameObject tutorialPanel = GameObject.Find("TutorialPanel");
        if (tutorialPanel == null)
        {
            EditorUtility.DisplayDialog("Error", "TutorialPanel not found!", "OK");
            return;
        }

        // Fix 1: Make background completely black (opaque)
        Image panelImage = tutorialPanel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.color = new Color(0, 0, 0, 1f); // Black, Alpha = 1 (completely opaque)
            Debug.Log("✅ Panel background set to solid black");
        }

        // Fix 2: Add Canvas component to override sorting
        Canvas canvas = tutorialPanel.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = tutorialPanel.AddComponent<Canvas>();
            Debug.Log("✅ Canvas component added");
        }
        
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100; // Display on top of everything
        Debug.Log("✅ Canvas sorting order set to 100");

        // Fix 3: Add GraphicRaycaster if not exists
        if (tutorialPanel.GetComponent<GraphicRaycaster>() == null)
        {
            tutorialPanel.AddComponent<GraphicRaycaster>();
            Debug.Log("✅ GraphicRaycaster added");
        }

        // Also fix Level Selection Panel if exists
        GameObject levelPanel = GameObject.Find("LevelSelectionPanel");
        if (levelPanel != null)
        {
            Image levelImage = levelPanel.GetComponent<Image>();
            if (levelImage != null)
            {
                levelImage.color = new Color(0, 0, 0, 1f);
            }

            Canvas levelCanvas = levelPanel.GetComponent<Canvas>();
            if (levelCanvas == null)
            {
                levelCanvas = levelPanel.AddComponent<Canvas>();
            }
            levelCanvas.overrideSorting = true;
            levelCanvas.sortingOrder = 100;

            if (levelPanel.GetComponent<GraphicRaycaster>() == null)
            {
                levelPanel.AddComponent<GraphicRaycaster>();
            }
            
            Debug.Log("✅ Level Selection Panel also fixed");
        }

        // Mark scene dirty
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        Debug.Log("✅ ALL FIXED!");
        EditorUtility.DisplayDialog("Success! 🎉", 
            "Tutorial Panel display fixed!\n\n" +
            "✅ Background now solid black\n" +
            "✅ Panel displays on top\n" +
            "✅ No more overlap with background text\n\n" +
            "Save scene and test!", 
            "Awesome!");
    }
}
