using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : UIScreen
{
    [SerializeField] private GameSaver _gameSaver;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private ScreenController _screenController;

    [Header("UI Elements")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private TextMeshProUGUI _deleteText;

    private void UpdateUI()
    {
        _masterSlider.value = _audioController.MasterVolume;
        _musicSlider.value = _audioController.MusicVolume;
        _sfxSlider.value = _audioController.SFXVolume;
    }

    public void BackToMenuScreen()
    {
        _backButton.interactable = false;
        _screenController.ShowScreen<MenuScreen>();
    }

    public override void OnShow()
    {
        _backButton.interactable = true;
        _deleteButton.interactable = true;
        _deleteText.text = "DELETE DATA";
        UpdateUI();
    }

    public void DeleteAllData()
    {
        _gameSaver.DeleteAllData();
        _deleteButton.interactable = false;
        _deleteText.text = "DELETED";
    }

    public void OnMasterVolumeChange(float value) => _audioController.MasterVolume = value;
    public void OnMusicVolumeChange(float value) => _audioController.MusicVolume = value;
    public void OnSFXVolumeChange(float value) => _audioController.SFXVolume = value;

    private void OnDisable()
    {
        _audioController.SaveAudioPreferences();
    }
}
