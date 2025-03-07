using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class UIPokemonInfo : MonoBehaviour
{
    private VisualElement root;
    private VisualElement PokemonIfoContainer;
    private bool hidden = true;
    
    private Label PokemonName;
    private VisualElement FrontImage;
    private VisualElement BackImage;
    private Label PokemonType;
    private Label PokemonHeight;
    private Label PokemonWeight;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        PokemonIfoContainer = root.Q<VisualElement>("PokemonIfoContainer");
        
        PokemonName = root.Q<Label>("PokemonName");
        FrontImage = root.Q<VisualElement>("FrontImage");
        BackImage = root.Q<VisualElement>("BackImage");
        PokemonType = root.Q<Label>("PokemonType");
        PokemonHeight = root.Q<Label>("PokemonHeight");
        PokemonWeight = root.Q<Label>("PokemonWeight");
        
        Button buttonClose = root.Q<Button>("ButtonClose");
        buttonClose.RegisterCallback<ClickEvent>(evt => CloseOpenInventory());
    }

    public void ShowPokemonInfo(Pokemon pokemon)
    {
        PokemonName.text = $"<b>{char.ToUpper(pokemon.name[0]) + pokemon.name.Substring(1)}</b>";
        
        PokemonType.text = $"<b>Type:</b> {string.Join(", ", pokemon.types.Select(t => char.ToUpper(t.type.name[0]) + t.type.name.Substring(1)))}";
        PokemonHeight.text = $"<b>Height:</b> {pokemon.height}";
        PokemonWeight.text = $"<b>Weight:</b> {pokemon.weight}";
        
        StartCoroutine(APIManager.Instance.LoadPokemonSprite(pokemon.sprites.front_default, tex => FrontImage.style.backgroundImage = new StyleBackground(tex)));
        StartCoroutine(APIManager.Instance.LoadPokemonSprite(pokemon.sprites.back_default, tex => BackImage.style.backgroundImage = new StyleBackground(tex)));
        
        CloseOpenInventory();
    }

    private void CloseOpenInventory()
    {
        if (!hidden) PokemonIfoContainer.AddToClassList("hidden");
        else PokemonIfoContainer.RemoveFromClassList("hidden");
        hidden = !hidden;
    }
}
