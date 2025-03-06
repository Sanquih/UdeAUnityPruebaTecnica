using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    #region Variables
    private List<int> itemsTaken = new List<int>();
    public List<int> ItemsTaken
    {
        get => itemsTaken;
    }
    #endregion

    #region Functions
    public event Action<int> OnItemTaken;

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
            OnItemTaken?.Invoke(itemsTaken.Count);
        }
    }
    #endregion

    #region Singleton
    public static ItemsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            itemsTaken = SaveManager.Instance.LoadItemsTaken();
            Debug.Log(ItemsTaken.Count);
        }
        else Destroy(gameObject);
    }
    #endregion
}
