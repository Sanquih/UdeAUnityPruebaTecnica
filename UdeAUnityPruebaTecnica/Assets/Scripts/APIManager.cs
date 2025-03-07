using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "https://pokeapi.co/api/v2/pokemon/";

    public IEnumerator GetPokemonData(int id, System.Action<Pokemon> callback)
    {
        string url = $"{apiUrl}{id}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Pokemon pokemon = JsonUtility.FromJson<Pokemon>(request.downloadHandler.text);
                callback?.Invoke(pokemon);
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
                callback?.Invoke(null);
            }
        }
    }

    public IEnumerator LoadPokemonSprite(string imageUrl, System.Action<Texture2D> callback)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                callback?.Invoke(texture);
            }
            else
            {
                Debug.LogError("Error al cargar la imagen: " + request.error);
                callback?.Invoke(null);
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
