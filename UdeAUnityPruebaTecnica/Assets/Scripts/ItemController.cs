using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private string id;

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Recogiste un objeto!");
        }
    }
}
