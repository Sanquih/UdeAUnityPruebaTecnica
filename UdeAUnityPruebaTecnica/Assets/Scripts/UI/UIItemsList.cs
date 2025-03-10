using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIItemsList : MonoBehaviour
{
    private VisualElement root;
    private VisualElement inventoryContent;
    private bool hidden = true;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement Inventory = root.Q<VisualElement>("Inventory");
        ScrollView inventoryScroll = Inventory.Q<ScrollView>("InventoryScroll");
        inventoryContent = inventoryScroll.Q<VisualElement>("InventoryContent");

        if (ItemsManager.Instance.ItemsTaken.Count > 0)
        {
            StartCoroutine(
                 ItemsManager.Instance.GetCurrentPokemons(
                     (pokemons) => CreateItemsList(pokemons)));
        }

        ItemsManager.Instance.OnPokemonInfo += AddItemList;

        Button buttonClose = Inventory.Q<Button>("ButtonClose");
        buttonClose.RegisterCallback<ClickEvent>(evt => CloseOpenInventory());

    }

    private void OnDisable()
    {
        ItemsManager.Instance.OnPokemonInfo -= AddItemList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) CloseOpenInventory();
    }

    private void CreateItemsList(List<Pokemon> items)
    {
        ClearInventoryContent();

        foreach (Pokemon item in items)
        {
            VisualElement itemElement = CreateItemsList(item);
            inventoryContent.Add(itemElement);
        }
    }

    public void ClearInventoryContent()
    {
        inventoryContent.Clear();
    }

    private VisualElement CreateItemsList(Pokemon item)
    {
        VisualElement newItem = new VisualElement();
        newItem.name = $"Item_{item.name}";
        newItem.AddToClassList("item-inventory");

        int scale = Screen.height / 100 * 22;
        int marginX = Screen.height / 100 * 5;
        int marginY = Screen.height / 100 * 4;

        newItem.style.height = scale;
        newItem.style.width = scale;
        newItem.style.marginBottom = marginY;
        newItem.style.marginTop = marginY;
        newItem.style.marginLeft = marginX;
        newItem.style.marginRight = marginX;

        Image itemImage = new Image();
        itemImage.AddToClassList("item-image");
        newItem.Add(itemImage);

        StartCoroutine(
            APIManager.Instance.LoadPokemonSprite(
                item.sprites.front_default,
                texture =>
                {
                    itemImage.style.backgroundImage = new StyleBackground(texture);
                }));

        newItem.RegisterCallback<ClickEvent>(
            evt => gameObject
            .GetComponent<UIPokemonInfo>()
            .ShowPokemonInfo(item));

        return newItem;
    }

    private void AddItemList(Pokemon item)
    {
        VisualElement itemElement = CreateItemsList(item);
        inventoryContent.Add(itemElement);
    }

    public void CloseOpenInventory()
    {
        VisualElement inventory = root.Q<VisualElement>("Inventory");
        if (!hidden) inventory.AddToClassList("hidden");
        else inventory.RemoveFromClassList("hidden");
        hidden = !hidden;
    }
}
