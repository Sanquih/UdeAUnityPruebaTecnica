using System.Collections.Generic;
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
        ScrollView inventoryScroll = root.Q<ScrollView>("InventoryScroll");
        inventoryContent = inventoryScroll.Q<VisualElement>("InventoryContent");

        CreateItemsList(ItemsManager.Instance.ItemsTaken);

        ItemsManager.Instance.OnItemTaken += AddItemList;

        Button buttonClose = root.Q<Button>("ButtonClose");
        buttonClose.RegisterCallback<ClickEvent>(evt => CloseOpenInventory());
    }

    private void OnDisable()
    {
        ItemsManager.Instance.OnItemTaken -= AddItemList;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            CloseOpenInventory();
            Debug.Log("¡Se presionó la tecla I!");
        }
    }

    private void CreateItemsList(List<int> items)
    {
        inventoryContent.Clear();

        foreach (int item in items)
        {
            VisualElement itemElement = CreateItemsList(item);
            inventoryContent.Add(itemElement);
        }
    }

    private VisualElement CreateItemsList(int item)
    {
        VisualElement newItem = new VisualElement();
        newItem.name = $"Item_{item}";
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

        return newItem;
    }

    private void AddItemList(int item)
    {
        VisualElement itemElement = CreateItemsList(item);
        inventoryContent.Add(itemElement);
    }

    private void CloseOpenInventory()
    {
        VisualElement inventory = root.Q<VisualElement>("Inventory");
        if (!hidden) inventory.AddToClassList("hidden");
        else inventory.RemoveFromClassList("hidden");
        hidden = !hidden;
    }
}
