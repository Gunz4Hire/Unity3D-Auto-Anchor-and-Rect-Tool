using UnityEngine;
using UnityEditor;

/// <summary>
/// AutoAnchorFromPosition
/// 
/// Unity Editor tool that automatically sets the anchors of selected UI elements
/// based on their current position and size within their parent RectTransform.
/// 
/// This helps maintain consistent layouts across different resolutions
/// by removing hardcoded positions and sizes.
/// 
/// Shortcut: Ctrl + Alt + A
/// </summary>
public class AutoAnchorRectTool : EditorWindow
{
    // Adds a menu item under "Tools" to trigger the auto anchoring
    [MenuItem("Tools/Auto Anchor UI %&a")]
    static void AutoAnchorUI()
    {
        // Loop through all selected GameObjects
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Try to get a RectTransform component
            RectTransform rect = obj.GetComponent<RectTransform>();
            if (rect == null || rect.parent == null)
                continue; // Skip if not a UI element or no parent

            // Ensure the RectTransform is inside a suitable Canvas
            Canvas canvas = rect.GetComponentInParent<Canvas>();
            if (canvas == null || canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.LogWarning($"{obj.name} is not in a suitable Canvas (needs Overlay or Screen Space - Camera). Skipped.");
                continue;
            }

            // Get the parent RectTransform
            RectTransform parent = rect.parent.GetComponent<RectTransform>();
            if (parent == null)
                continue;

            // Record Undo step for safety
            Undo.RecordObject(rect, "Auto Anchor");

            // Get the parent's size
            Vector2 parentSize = parent.rect.size;
            // Get the pivot of the RectTransform
            Vector2 pivot = rect.pivot;
            // Get local position and size
            Vector2 localPos = rect.localPosition
