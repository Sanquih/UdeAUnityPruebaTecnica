using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private int id;

    private void Awake()
    {
        if (ItemsManager.Instance.IsItemTaken(id)) Disappear(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"¡Recogiste un objeto! ID: {id}");
            Disappear();
            ItemsManager.Instance.AddItemTaken(id);
        }
    }

    private void Disappear(bool inAwake = false)
    {
        if (inAwake)
        {
            Destroy(gameObject);
            return;
        }

        GetComponent<MeshCollider>().enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);


        ParticleSystem ps = GetComponent<ParticleSystem>();
        var mainModule = ps.main;
        mainModule.loop = false;
    }
}
