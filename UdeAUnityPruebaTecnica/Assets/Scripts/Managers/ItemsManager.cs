
using System;
using System.Collections;
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

    public List<Pokemon> Pokemons = new List<Pokemon>();
    #endregion

    #region Functions
    public event Action<int> OnItemTaken;
    public event Action<Pokemon> OnPokemonInfo;

    public IEnumerator GetCurrentPokemons(Action<List<Pokemon>> callback)
    {
        Pokemons = new List<Pokemon>();

        foreach (int id in itemsTaken)
        {
            yield return StartCoroutine(APIManager.Instance.GetPokemonData(id, (pokemon) =>
            {
                if (pokemon != null)
                {
                    Pokemons.Add(pokemon);
                }
            }));
        }

        callback?.Invoke(Pokemons);
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
            OnItemTaken?.Invoke(itemsTaken.Count);
            StartCoroutine(APIManager.Instance.GetPokemonData(id, (pokemon) =>
            {
                Pokemons.Add(pokemon);
                OnPokemonInfo?.Invoke(pokemon);
            }));
        }
    }

    public void ClearItemsTaken()
    {
        itemsTaken = new List<int>();
        Pokemons = new List<Pokemon>();
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
        }
        else Destroy(gameObject);
    }
    #endregion
}
