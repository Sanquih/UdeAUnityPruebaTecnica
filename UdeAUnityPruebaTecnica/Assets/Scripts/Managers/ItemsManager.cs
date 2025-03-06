using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    private List<int> itemsTaken = new List<int>();
    public List<int> ItemsTaken
    {
        get => itemsTaken;
    }

    public bool IsItemTaken(int id)
    {
        return itemsTaken.Contains(id);
    }

    public void AddItemTaken(int id)
    {
        if (!itemsTaken.Contains(id))
        {
            itemsTaken.Add(id);
            SaveManager.Instance.SaveItemsTaken(itemsTaken);
        }
    }

    public static ItemsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        itemsTaken = SaveManager.Instance.LoadItemsTaken();
        Debug.Log(ItemsTaken.Count);
    }
}
