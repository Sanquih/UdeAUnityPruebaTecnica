using UnityEngine;
using UnityEngine.UIElements;

public class UIItemCounter : MonoBehaviour
{
    private Label currenItemsAmount;
    private Label totalItems;
    private int totalItemsAmount = 0;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        currenItemsAmount = root.Q<Label>("CurrenItemsAmount");
        totalItems = root.Q<Label>("TotalItems");

        ItemsManager.Instance.OnItemTaken += SetCurrenItemsAmount;

        SetTotalItems();
        SetCurrenItemsAmount(ItemsManager.Instance.ItemsTaken.Count);
    }

    private void OnDisable()
    {
        ItemsManager.Instance.OnItemTaken -= SetCurrenItemsAmount;
    }

    public void SetCurrenItemsAmount(int newAmount)
    {
        if (newAmount >= totalItemsAmount) currenItemsAmount.text = totalItemsAmount.ToString();
        else currenItemsAmount.text = $"{newAmount.ToString()} ";
    }

    private void SetTotalItems()
    {
        GameObject items = GameObject.Find("Items");

        if (items != null) totalItemsAmount = items.transform.childCount;
        totalItems.text = $"/{totalItemsAmount}";
    }
}
