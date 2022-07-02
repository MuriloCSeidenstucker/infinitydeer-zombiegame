using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameSaver _gameSaver;
    [SerializeField] private AudioMixer _mixer;

    private const string c_masterVolumeParameter = "MasterVolume";
    private const string c_musicVolumeParameter = "MusicVolume";
    private const string c_sfxVolumeParameter = "SFXVolume";

    private const int c_minVolumeDb = -60;
    private const int c_maxVolumeDb = 0;

    public float MasterVolume
    {
        get => GetMixerVolumeParameter(c_masterVolumeParameter);
        set => SetMixerVolumeParameter(c_masterVolumeParameter, value);
    }

    public float MusicVolume
    {
        get => GetMixerVolumeParameter(c_musicVolumeParameter);
        set => SetMixerVolumeParameter(c_musicVolumeParameter, value);
    }

    public float SFXVolume
    {
        get => GetMixerVolumeParameter(c_sfxVolumeParameter);
        set => SetMixerVolumeParameter(c_sfxVolumeParameter, value);
    }

    private void Start()
    {
        LoadAudioPreferences();
    }

    private void SetMixerVolumeParameter(string key, float volumePercent)
    {
        float volumeValue = Mathf.Lerp(c_minVolumeDb, c_maxVolumeDb, volumePercent);
        _mixer.SetFloat(key, volumeValue);
    }

    private float GetMixerVolumeParameter(string key)
    {
        if (_mixer.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(c_minVolumeDb, c_maxVolumeDb, value);
        }
        return 1.0f;
    }

    private void LoadAudioPreferences()
    {
        _gameSaver.LoadGame();
        MasterVolume = _gameSaver.AudioPreferences.MasterVolume;
        MusicVolume = _gameSaver.AudioPreferences.MusicVolume;
        SFXVolume = _gameSaver.AudioPreferences.SFXVolume;
    }

    public void SaveAudioPreferences()
    {
        _gameSaver.SaveAudioPreferences(new AudioPreferences
        {
            MasterVolume = MasterVolume,
            MusicVolume = MusicVolume,
            SFXVolume = SFXVolume,
        });
    }
}
