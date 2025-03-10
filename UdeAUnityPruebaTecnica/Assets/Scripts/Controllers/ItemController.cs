using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private AudioClip[] audios;
    private AudioSource audioSource;

    private void Awake()
    {
        if (ItemsManager.Instance.IsItemTaken(id)) Disappear();
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

    private void Disappear()
    {
        GetComponent<MeshCollider>().enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);


        ParticleSystem ps = GetComponent<ParticleSystem>();
        var mainModule = ps.main;
        mainModule.loop = false;

        FadeOutAudio(audioSource);
    }

    public void Appear()
    {
        GetComponent<MeshCollider>().enabled = true;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);


        ParticleSystem ps = GetComponent<ParticleSystem>();
        var mainModule = ps.main;
        mainModule.loop = true;
        ps.Play();

        PlayRandomAudio();
        FadeOutAudio(audioSource, 1f, 1f);
    }

    public void FadeOutAudio(AudioSource audioSource, float duration = 1f, float endVol = 0f)
    {
        if (audioSource != null)
            StartCoroutine(FadeOutCoroutine(audioSource, duration, endVol));
    }

    private IEnumerator FadeOutCoroutine(AudioSource audioSource, float duration, float endVol)
    {
        float startVolume = audioSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, endVol, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = endVol;
        if (endVol <= 0f) audioSource.Stop();
    }
}
