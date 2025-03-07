using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class UIHelpers : MonoBehaviour
{
    VisualElement root;
    UIDocument uiDocument;

    float previousWidthFactor = -1f;
    float previousHeightFactor = -1f;

    private bool isAnimating = true;

    private List<string> keysPressed = new List<string>();

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        ItemsManager.Instance.OnPokemonInfo += ShowName;

        AdjustKeysSize();
        StartKeyAnimation();
        RegisterResizeCallback();
    }

    private void OnDisable()
    {
        ItemsManager.Instance.OnPokemonInfo -= ShowName;
    }

    private void Update()
    {
        if (!isAnimating) return;

        bool updated = false;

        if (Input.GetKeyDown(KeyCode.A)) { keysPressed.Add("A"); updated = true; }
        if (Input.GetKeyDown(KeyCode.W)) { keysPressed.Add("W"); updated = true; }
        if (Input.GetKeyDown(KeyCode.D)) { keysPressed.Add("D"); updated = true; }
        if (Input.GetKeyDown(KeyCode.E))
        {
            keysPressed.Add("E");
            Disappear("KeysInventory");
            updated = true;
        }

        if (updated) CheckConditions();
    }

    private void CheckConditions()
    {
        bool hasAWD = keysPressed.Contains("A") && keysPressed.Contains("W") && keysPressed.Contains("D");

        if (hasAWD) Disappear("KeysMuve");

        if (hasAWD && keysPressed.Contains("E")) StopKeyAnimation();
    }

    private void ShowName(Pokemon pokemon)
    {
        Label nameLabel = new Label($"<b>{char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1)}</b>");
        nameLabel.AddToClassList("label-little");
        nameLabel.AddToClassList("background-gray");
        nameLabel.AddToClassList("helper-name");

        nameLabel.style.position = Position.Absolute;
        nameLabel.style.right = Length.Percent(10);
        nameLabel.style.opacity = 1f;

        root.Add(nameLabel);

        StartCoroutine(AnimateLabel(nameLabel));
    }

    private IEnumerator AnimateLabel(Label label)
    {
        float duration = 3f;
        float elapsedTime = 0f;
        float startTop = 100f;
        float endTop = startTop - 50f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            label.style.top = Length.Percent(Mathf.Lerp(startTop, endTop, progress));
            label.style.opacity = Mathf.Lerp(1.1f, 0f, progress);

            yield return null;
        }

        root.Remove(label);
    }

    private void AdjustKeysSize()
    {
        const float baseWidth = 1920f;
        const float baseHeight = 1080f;
        const float baseKeys = 150f;
        const float baseKeysMuveWidth = 500f;
        const float baseKeysMuveHeight = 450f;
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
        var keysMuve = root.Q<VisualElement>("KeysMuve");
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

        keysMuve.style.width = baseKeysMuveWidth * widthFactor;
        keysMuve.style.height = baseKeysMuveHeight * heightFactor;

        keysInventory.style.width = baseKeysInventoryWidth * widthFactor;
        keysInventory.style.height = baseKeysInventoryHeight * heightFactor;
    }

    private void RegisterResizeCallback()
    {
        var root = uiDocument.rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(evt => AdjustKeysSize());
    }

    private void StartKeyAnimation()
    {
        var keys = root.Query<VisualElement>().Class("key").Build();

        foreach (var key in keys)
        {
            StartCoroutine(AnimateKey(key));
        }
    }

    private IEnumerator AnimateKey(VisualElement key)
    {
        while (isAnimating)
        {
            float delay = Random.Range(0.5f, 1f);

            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(FadeOpacity(key, 1f, 0.2f, 0.3f));
            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(FadeOpacity(key, 0.2f, 1f, 0.3f));
        }
    }

    private IEnumerator FadeOpacity(VisualElement element, float startOpacity, float endOpacity, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            element.style.opacity = Mathf.Lerp(startOpacity, endOpacity, t);
            yield return null;
        }

        element.style.opacity = endOpacity;
    }

    private void StopKeyAnimation()
    {
        isAnimating = false;
    }

    private void Disappear(string id)
    {
        var keysContainer = root.Q<VisualElement>(id);
        if (keysContainer != null)
        {
            StartCoroutine(FadeOpacity(keysContainer, keysContainer.resolvedStyle.opacity, 0f, 1f));
        }
    }

}
