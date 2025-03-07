using UnityEngine;
using UnityEngine.UIElements;

public class UIFontSize : MonoBehaviour
{
    private UIDocument uiDocument;

    private const float baseWidth = 1920f;
    private const float baseFontSizeNormal = 70f;
    private const float baseFontSizeTitle = 100f;
    private float previousWidthFactor = -1f;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("FontSize script requires a UIDocument component.");
            return;
        }

        AdjustFontSize();
        RegisterResizeCallback();
    }

    private void AdjustFontSize()
    {
        if (uiDocument == null) return;

        float widthFactor = Mathf.Clamp(Screen.width / baseWidth, 0.5f, 2f); // Safe scaling between 50% and 200%
        if (Mathf.Approximately(widthFactor, previousWidthFactor)) return; // Avoid unnecessary changes
        previousWidthFactor = widthFactor;

        var root = uiDocument.rootVisualElement;
        var labelsNormal = root.Query<Label>().Class("label-normal").Build();
        var labelsTitle = root.Query<Label>().Class("label-title").Build();

        foreach (var label in labelsNormal)
        {
            label.style.fontSize = baseFontSizeNormal * widthFactor;
        }

        foreach (var label in labelsTitle)
        {
            label.style.fontSize = baseFontSizeTitle * widthFactor;
        }
    }

    private void RegisterResizeCallback()
    {
        var root = uiDocument.rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(evt => AdjustFontSize());
    }
}
