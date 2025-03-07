using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private AudioClip[] audios;
    private AudioSource audioSource;

    private void Awake()
    {
        if (ItemsManager.Instance.IsItemTaken(id)) Disappear(true);
        else PlayRandomAudio();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Disappear();
            ItemsManager.Instance.AddItemTaken(id);
        }
    }

    private void PlayRandomAudio()
    {
        audioSource = GetComponent<AudioSource>();

        if (audios.Length > 0)
        {
            int randomIndex = Random.Range(0, audios.Length);
            audioSource.clip = audios[randomIndex];
            audioSource.Play();
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

        FadeOutAudio(audioSource);
    }

    public void FadeOutAudio(AudioSource audioSource, float duration = 1f)
    {
        StartCoroutine(FadeOutCoroutine(audioSource, duration));
    }

    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
