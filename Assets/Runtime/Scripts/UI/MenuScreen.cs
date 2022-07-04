using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private ScreenController _screenController;

    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;

    public override void OnShow()
    {
        _startButton.interactable = true;
        _settingsButton.interactable = true;
        _quitButton.interactable = true;
    }

    public void OnQuitButtonPress()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnStartButtonPress() => _gameMode.StartGame();
    public void OnSettingsButtonPress() => _screenController.ShowScreen<SettingsScreen>();
}
