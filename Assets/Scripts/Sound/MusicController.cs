using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [System.Serializable]
    public class AudioEffectData
    {
        public string effect;
        public AudioClip audioSource;
    }

    //Musics - Have 2 Source, 1 active music and 2 is the next music with fade effect.
    [Header("Music")]
    [SerializeField, Range(0f, 5f)] private float durationFade = 2f;
    [SerializeField] private AudioClip[] _clips;
    private AudioSource _source, _source2;
    private bool _playMusic = true;
    //Instancia 
    public static MusicController instance;
    [Header("Effects")]
    [SerializeField] private AudioMixerGroup effectsMixerGroup;
    [SerializeField] private List<AudioEffectData> audioEffects = new List<AudioEffectData>();
    void Start()
    {
        if(instance != null && instance != this)
            Destroy(this);
        instance = this;
        _source = GetComponent<AudioSource>();
        _source2 = gameObject.AddComponent<AudioSource>();
        _source2.outputAudioMixerGroup = _source.outputAudioMixerGroup;
        LaunchMusic();
    }
    //MUSIC WITH FADE
    void LaunchMusic()
    {
        if (_clips != null)
        {
            
            _source.Stop();
            _source.clip = _clips[Random.Range(0, _clips.Length)];
            _source.Play();
            Invoke("FadeMusic", _source.clip.length-durationFade);
        }
    }
    void FadeMusic()
    {
        CancelInvoke();
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        if( _playMusic )
        {
            _source2.Stop();
            _source2.clip = _clips[Random.Range(0, _clips.Length)];
            _source2.volume = 0;
            _source2.Play();

            float currentTime = 0f;

            while( currentTime < durationFade )
            {
                _source.volume = Mathf.Lerp(1, 0, currentTime/durationFade);
                _source2.volume = 1 - _source.volume;
                yield return null;
                currentTime += Time.deltaTime;
            }
            _source.Stop();
            _source2.volume = 1f;
            
        }
        else
        {
            // 2 -> 1
            _source.Stop();
            _source.clip = _clips[Random.Range(0, _clips.Length)];
            _source.volume = 0;
            _source.Play();

            float currentTime = 0f;

            while (currentTime < durationFade)
            {
                _source2.volume = Mathf.Lerp(1, 0, currentTime / durationFade);
                _source.volume = 1 - _source2.volume;
                yield return null;
                currentTime += Time.deltaTime;
            }
            _source2.Stop();
            _source.volume = 1f;
        }
        _playMusic = !_playMusic;
    }
    //EFFECTS
    public void PlayEffect(string effect)
    {
        AudioClip clip = audioEffects.Find(ae => ae.effect == effect)?.audioSource;
        if (clip == null)
        {
            Debug.LogWarning($"No se encontró el efecto: {effect}");
            return;
        }
        SFXManager.Instance.PlaySFX(clip);
/*
        GameObject tempGO = new GameObject($"SFX_{effect}");
        tempGO.transform.position = Vector3.zero;

        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.clip = clip;
        aSource.outputAudioMixerGroup = effectsMixerGroup;
        aSource.Play();

        Destroy(tempGO, clip.length);
*/
    }

}
