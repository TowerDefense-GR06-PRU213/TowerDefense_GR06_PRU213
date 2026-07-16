using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

public class FixTutorialButton
{
    [MenuItem("Tools/Fix Tutorial Button 🔧")]
    public static void FixButton()
    {
        // Find Tutorial Button
        GameObject tutorialBtn = GameObject.Find("TutorialButton");
        if (tutorialBtn == null)
        {
            EditorUtility.DisplayDialog("Error", "TutorialButton not found!", "OK");
            return;
        }

        Button button = tutorialBtn.GetComponent<Button>();
        if (button == null)
        {
            EditorUtility.DisplayDialog("Error", "Button component not found!", "OK");
            return;
        }

        // Find MainMenuController
        MainMenuController controller = Object.FindObjectOfType<MainMenuController>();
        if (controller == null)
        {
            EditorUtility.DisplayDialog("Error", "MainMenuController not found!", "OK");
            return;
        }

        // Clear old listeners
        button.onClick.RemoveAllListeners();

        // Add new listener
        UnityAction action = new UnityAction(controller.OpenTutorial);
        button.onClick.AddListener(action);

        // Mark scene dirty
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        Debug.Log("✅ Tutorial Button fixed! Now calls OpenTutorial()");
        EditorUtility.DisplayDialog("Success!", 
            "Tutorial Button fixed!\n\n" +
            "Now clicking 'HOW TO PLAY' will open Tutorial Panel.\n\n" +
            "Save scene and test!", 
            "OK");
    }
}
