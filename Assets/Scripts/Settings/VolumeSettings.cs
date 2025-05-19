using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]private Slider general, music, effects;
    [SerializeField] private AudioMixer audioMixer;
    private void Start()
    {
        float vol = PlayerPrefs.GetFloat("Master");
        audioMixer.SetFloat("Master", vol);    
        general.value = Mathf.Pow(10, vol / 20);
        vol = PlayerPrefs.GetFloat("Music");
        audioMixer.SetFloat("Music", vol);
        music.value = Mathf.Pow(10, vol / 20);
        vol = PlayerPrefs.GetFloat("Effects");
        audioMixer.SetFloat("Effects", vol);
        effects.value = Mathf.Pow(10, vol / 20);
    }
    
    public void SetVolume(float volume)
    {
        if (volume.Equals(0))
        {// -80 dB es muted en Unity
            volume = -80f;
            audioMixer.SetFloat("Master", volume);
            
        }
        else
        {// Convertir el valor del slider a decibelios
            volume = Mathf.Log10(volume) * 20;
            audioMixer.SetFloat("Master", volume);
        }
        PlayerPrefs.SetFloat("Master", volume);
    }
    public void SetMusic(float volume)
    {
        if (volume.Equals(0))
        {
            volume = -80f;
            audioMixer.SetFloat("Music", volume);
        }
        else
        {
            volume = Mathf.Log10(volume) * 20;
            audioMixer.SetFloat("Music", volume);
        }
        PlayerPrefs.SetFloat("Music", volume);
    }
    public void SetEffects(float volume)
    {
        if (volume.Equals(0))
        {
            volume = -80f;
            audioMixer.SetFloat("Effects", volume);
        }
        else
        {
            volume = Mathf.Log10(volume) * 20;
            audioMixer.SetFloat("Effects", volume);
        }
        PlayerPrefs.SetFloat("Effects", volume);
    }
}
