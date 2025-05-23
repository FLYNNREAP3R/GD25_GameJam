using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioMixerGroup outputGroup;
    private List<AudioSource> sources = new();

    void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = GetAvailableSource();
        source.outputAudioMixerGroup = outputGroup;
        source.clip = clip;
        source.Play();
    }

    private AudioSource GetAvailableSource()
    {
        foreach (var s in sources)
        {
            if (!s.isPlaying)
                return s;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.spatialBlend = 0f;
        newSource.playOnAwake = false;
        sources.Add(newSource);
        return newSource;
    }
}
