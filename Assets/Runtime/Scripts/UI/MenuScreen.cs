using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private ScreenController _screenController;

    [Header("Buttons")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;

    public override void OnShow()
    {
        _startButton.interactable = true;
        _settingsButton.interactable = true;
    }

    public void OnStartButtonPress() => _gameMode.StartGame();
    public void OnSettingsButtonPress() => _screenController.ShowScreen<SettingsScreen>();
}
