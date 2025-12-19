using UnityEngine;

/// <summary>
/// Simple drop-in music player. Add to any scene object, assign a clip, and it will configure
/// an AudioSource to play (looped by default).
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField] bool playOnStart = true;
    [SerializeField] bool loop = true;
    [Range(0f, 1f)] [SerializeField] float volume = 0.8f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ConfigureSource();
    }

    void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }

    /// <summary>Play the assigned clip (if any).</summary>
    public void Play()
    {
        if (musicClip == null)
        {
            Debug.LogWarning("BackgroundMusic has no AudioClip assigned.", this);
            return;
        }

        ConfigureSource();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>Stop playback.</summary>
    public void Stop()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void ConfigureSource()
    {
        audioSource.clip = musicClip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.playOnAwake = false; // we control start via playOnStart
    }
}
