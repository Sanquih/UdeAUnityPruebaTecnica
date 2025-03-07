using UnityEngine;
using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private bool cleanData = false;

    private string ITEMS_TAKEN_KEY = "ITEMSTAKEN";
    public List<int> LoadItemsTaken()
    {
        return SaveGame.Exists(ITEMS_TAKEN_KEY)
        ? SaveGame.Load<List<int>>(ITEMS_TAKEN_KEY)
        : new List<int>();
    }
    public void SaveItemsTaken(List<int> value)
    {
        SaveGame.Save(ITEMS_TAKEN_KEY, value);
    }

    public void RestartGame()
    {
        SaveGame.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static SaveManager Instance;
    private void Awake()
    {
        if (cleanData) SaveGame.Clear();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
