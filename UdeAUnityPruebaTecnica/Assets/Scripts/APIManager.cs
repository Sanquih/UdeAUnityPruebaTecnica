using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "https://pokeapi.co/api/v2/pokemon/";

    void Start()
    {
        for (int i = 1; i <= 50; i++)
        {
            // StartCoroutine(GetPokemonData(i));
        }
    }

    IEnumerator GetPokemonData(int id)
    {
        string url = $"https://pokeapi.co/api/v2/pokemon/{id}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Debug.Log($"Datos del Pokémon {id}: {request.downloadHandler.text}");
                Pokemon pokemon = JsonUtility.FromJson<Pokemon>(request.downloadHandler.text);
                // Debug.Log($"Nombre: {pokemon.name}, ID: {pokemon.id}");
                // StartCoroutine(LoadPokemonSprite(pokemon.sprites.front_default, GetComponent<Renderer>()));
            }
            else
            {
                Debug.LogError($"Error al obtener el Pokémon {id}: {request.error}");
            }
        }
    }

    IEnumerator LoadPokemonSprite(string imageUrl, Renderer renderer)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                renderer.material.mainTexture = texture;
            }
            else
            {
                Debug.LogError("Error al cargar la imagen: " + request.error);
            }
        }
    }

    public static APIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}

[System.Serializable]
public class Pokemon
{
    public string name;
    public int id;
    public SpriteInfo sprites;
}

[System.Serializable]
public class SpriteInfo
{
    public string front_default;
}