#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class AutoAnchorFromPosition : EditorWindow
{
    [MenuItem("Tools/Auto Anchor UI %&a")]
    static void AutoAnchorUI()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            RectTransform rect = obj.GetComponent<RectTransform>();
            if (rect == null || rect.parent == null)
                continue;

            Canvas canvas = rect.GetComponentInParent<Canvas>();
            if (canvas == null || canvas.renderMode == RenderMode.WorldSpace)
            {
                Debug.LogWarning($"{obj.name} is not in a suitable Canvas (needs Overlay or Screen Space - Camera). Skipped.");
                continue;
            }

            RectTransform parent = rect.parent.GetComponent<RectTransform>();
            if (parent == null)
                continue;

            Undo.RecordObject(rect, "Auto Anchor");

            Vector2 parentSize = parent.rect.size;
            Vector2 pivot = rect.pivot;

            Vector2 localPos = rect.localPosition;
            Vector2 size = rect.rect.size;

            Vector2 min = (localPos - Vector2.Scale(size, pivot)) / parentSize + parent.pivot;
            Vector2 max = min + size / parentSize;

            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = Vector2.zero;

            Debug.Log($"✅ Anchors set on: {obj.name}");
        }
    }
}
#endif
