using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIResetButton : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button restartProgress = root.Q<Button>("RestartProgress");
        restartProgress.RegisterCallback<ClickEvent>(evt => RestartProgress());
    }

    private void RestartProgress()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().Init();
        ItemsManager.Instance.ClearItemsTaken();
        SaveManager.Instance.ClearData();
        GameObject.Find("UIInventory").GetComponent<UIItemsList>().ClearInventoryContent();
        GameObject.Find("UIInventory").GetComponent<UIItemsList>().CloseOpenInventory();
        GameObject.Find("UIMain").GetComponent<UIItemCounter>().SetCurrenItemsAmount(0);
        Camera.main.transform.GetComponent<CameraFollow>().ResetCamera();

        ItemController[] items = FindObjectsByType<ItemController>(FindObjectsSortMode.None);
        foreach (ItemController item in items) item.Appear();
    }
}