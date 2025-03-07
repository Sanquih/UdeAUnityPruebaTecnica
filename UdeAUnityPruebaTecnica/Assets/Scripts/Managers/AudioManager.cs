using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSourcePrefab;
    public int maxSfxSources = 10;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;

    private List<AudioSource> sfxSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        sfxSources = new List<AudioSource>();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip musicClip, bool loop = true, float volume = 0.4f)
    {
        if (musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = loop;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }

    public void ContinueMusic()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    public void rebootMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();  // Detiene la reproducci�n actual
            musicSource.time = 0f; // Reinicia la posici�n al inicio
            musicSource.Play(); // Vuelve a reproducir la m�sica desde el principio
        }
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1f)
    {
        AudioSource sfxSource = GetAvailableSfxSource();
        if (sfxSource != null)
        {
            sfxSource.clip = sfxClip;
            sfxSource.volume = volume;
            sfxSource.Play();
        }
    }


    private AudioSource GetAvailableSfxSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
                return source;
        }

        if (sfxSources.Count < maxSfxSources)
        {
            AudioSource newSource = Instantiate(sfxSourcePrefab, transform);
            sfxSources.Add(newSource);
            return newSource;
        }

        return null;
    }
}
