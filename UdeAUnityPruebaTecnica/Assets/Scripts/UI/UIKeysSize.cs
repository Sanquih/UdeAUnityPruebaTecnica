using UnityEngine;
using UnityEngine.UIElements;

public class UIKeysSize : MonoBehaviour
{
    VisualElement root;
    UIDocument uiDocument;

    float previousWidthFactor = -1f;
    float previousHeightFactor = -1f;

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        AdjustKeysSize();
        RegisterResizeCallback();
    }

    private void AdjustKeysSize()
    {
        const float baseWidth = 1920f;
        const float baseHeight = 1080f;
        const float baseKeys = 150f;
        const float baseKeysMoveWidth = 500f;
        const float baseKeysMoveHeight = 450f;
        const float baseKeysInventoryWidth = 500f;
        const float baseKeysInventoryHeight = 300f;
        const float baseBorderSolid = 11f;

        float widthFactor = Mathf.Clamp(Screen.width / baseWidth, 0.5f, 2f);
        if (Mathf.Approximately(widthFactor, previousWidthFactor)) return;
        previousWidthFactor = widthFactor;

        float heightFactor = Mathf.Clamp(Screen.height / baseHeight, 0.5f, 2f);
        if (Mathf.Approximately(heightFactor, previousHeightFactor)) return;
        previousHeightFactor = widthFactor;

        var keys = root.Query<VisualElement>().Class("key").Build();
        var keysMove = root.Q<VisualElement>("KeysMove");
        var keysInventory = root.Q<VisualElement>("KeysInventory");

        foreach (var key in keys)
        {
            key.style.width = baseKeys * widthFactor;
            key.style.height = baseKeys * widthFactor;
            key.style.borderTopWidth = baseBorderSolid * widthFactor;
            key.style.borderBottomWidth = baseBorderSolid * widthFactor;
            key.style.borderLeftWidth = baseBorderSolid * widthFactor;
            key.style.borderRightWidth = baseBorderSolid * widthFactor;
        }

        keysMove.style.width = baseKeysMoveWidth * widthFactor;
        keysMove.style.height = baseKeysMoveHeight * heightFactor;

        keysInventory.style.width = baseKeysInventoryWidth * widthFactor;
        keysInventory.style.height = baseKeysInventoryHeight * heightFactor;
    }

    private void RegisterResizeCallback()
    {
        var root = uiDocument.rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(evt => AdjustKeysSize());
    }

}
