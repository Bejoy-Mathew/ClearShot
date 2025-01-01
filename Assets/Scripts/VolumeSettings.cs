using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer Mixer;
    [SerializeField] private Slider BgmSlider;    
    [SerializeField] private Slider SfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("bgmvolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSfxVolume();
        }
    }

    public void SetBGMVolume()
    {
        float volume = BgmSlider.value;
        Mixer.SetFloat("BGM", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("bgmvolume", volume);
    }

    public void SetSfxVolume()
    {
        float volume = SfxSlider.value;
        Mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxvolume", volume);
    }

    private void LoadVolume()
    {
        BgmSlider.value = PlayerPrefs.GetFloat("bgmvolume");
        SfxSlider.value = PlayerPrefs.GetFloat("sfxvolume");
        SetBGMVolume();
        SetSfxVolume();
    }
    
}
