using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class DebugTutorialPanel
{
    [MenuItem("Tools/Debug Tutorial Panel 🔍")]
    public static void DebugPanel()
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        var rootObjects = scene.GetRootGameObjects();
        GameObject tutorialPanel = null;
        foreach (var root in rootObjects)
        {
            tutorialPanel = FindChildRecursive(root, "TutorialPanel");
            if (tutorialPanel != null) break;
        }

        if (tutorialPanel == null)
        {
            Debug.LogError("❌ [Debug] TutorialPanel NOT found in scene!");
            return;
        }

        Debug.Log($"====== DEBUGGING {tutorialPanel.name} ======");
        Debug.Log($"Active Self: {tutorialPanel.activeSelf}");
        Debug.Log($"Active In Hierarchy: {tutorialPanel.activeInHierarchy}");

        RectTransform rectTransform = tutorialPanel.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Debug.Log($"[RectTransform] AnchorMin: {rectTransform.anchorMin}, AnchorMax: {rectTransform.anchorMax}");
            Debug.Log($"[RectTransform] AnchoredPosition: {rectTransform.anchoredPosition}, SizeDelta: {rectTransform.sizeDelta}");
            Debug.Log($"[RectTransform] LocalScale: {rectTransform.localScale}, Rect: {rectTransform.rect}");
        }
        else
        {
            Debug.LogError("[RectTransform] Component MISSING!");
        }

        Image img = tutorialPanel.GetComponent<Image>();
        if (img != null)
        {
            Debug.Log($"[Image] Enabled: {img.enabled}");
            Debug.Log($"[Image] Color: {img.color}");
            Debug.Log($"[Image] Sprite: {(img.sprite != null ? img.sprite.name : "None")}");
            Debug.Log($"[Image] Material: {(img.material != null ? img.material.name : "Default")}");
            Debug.Log($"[Image] Type: {img.type}");
        }
        else
        {
            Debug.LogError("[Image] Component MISSING!");
        }

        Canvas canvas = tutorialPanel.GetComponent<Canvas>();
        if (canvas != null)
        {
            Debug.Log($"[Canvas] Enabled: {canvas.enabled}");
            Debug.Log($"[Canvas] OverrideSorting: {canvas.overrideSorting}");
            Debug.Log($"[Canvas] SortingOrder: {canvas.sortingOrder}");
            Debug.Log($"[Canvas] RenderMode: {canvas.renderMode}");
        }
        else
        {
            Debug.Log("[Canvas] Component not present (normal if not sub-canvas)");
        }

        CanvasRenderer renderer = tutorialPanel.GetComponent<CanvasRenderer>();
        if (renderer != null)
        {
            Debug.Log($"[CanvasRenderer] Cull: {renderer.cull}");
        }
        else
        {
            Debug.LogError("[CanvasRenderer] Component MISSING!");
        }

        // Print children info
        Debug.Log($"--- Children ({tutorialPanel.transform.childCount}) ---");
        for (int i = 0; i < tutorialPanel.transform.childCount; i++)
        {
            Transform child = tutorialPanel.transform.GetChild(i);
            Debug.Log($"Child {i}: {child.name} (Active: {child.gameObject.activeSelf})");
        }
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
}
