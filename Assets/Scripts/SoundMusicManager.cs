using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _MasterSliderVolume;
    [SerializeField] private Slider _MusicSliderVolume;
    [SerializeField] private Slider _SFXSliderVolume;

    void Awake()
    {
        SoundMusicManager[] objs = FindObjectsOfType<SoundMusicManager>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        //Récupération des playerprefs
        float _MasterVolume = PlayerPrefs.GetFloat("MasterVolumeMixer");
        float _MusicVolume = PlayerPrefs.GetFloat("MusicVolumeMixer");
        float _SFXVolume = PlayerPrefs.GetFloat("SFXVolumeMixer");

        //Attribution des valeurs aux sliders
        _MasterSliderVolume.value = _MasterVolume;
        _MusicSliderVolume.value = _MusicVolume;
        _SFXSliderVolume.value = _SFXVolume;

        //Attributions des valeurs aux variables exposées de l'audiomixer
        _audioMixer.SetFloat("Master", _MasterVolume);
        _audioMixer.SetFloat("Music", _MusicVolume);
        _audioMixer.SetFloat("Effects", _SFXVolume);
    }

    //void Update()
    //{
    //    //Récupération des sliders
    //    float _MusicVolume = _MusicSliderVolume.value;
    //    float _SFXVolume = _SFXSliderVolume.value;

    //    //Attributions des valeurs à l'audiomixer
    //    _audioMixer.SetFloat("Music", _MusicVolume);
    //    _audioMixer.SetFloat("Effects", _SFXVolume);

    //    //Sauvegarde des prefs
    //    PlayerPrefs.SetFloat("MusicVolumeMixer", _MusicVolume);
    //    PlayerPrefs.SetFloat("SFXVolumeMixer", _SFXVolume);
    //}

    public void UpdateMaster(float value)
	{
        _audioMixer.SetFloat("Master", value);
        PlayerPrefs.SetFloat("MasterVolumeMixer", value);
    }

    public void UpdatMusic(float value)
	{
        _audioMixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat("MusicVolumeMixer", value);
    }

    public void UpdateSFX(float value)
	{
        _audioMixer.SetFloat("Effects", value);
        PlayerPrefs.SetFloat("SFXVolumeMixer", value);
    }
}
