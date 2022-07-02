using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameSaver _gameSaver;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private MusicPlayer _musicPlayer;
    [SerializeField] private ScreenController _screenController;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private float _reloadGameTime = 1.0f;
    [SerializeField] private float _gameOverTime = 1.0f;
    [SerializeField] private float _startGameTime = 2.0f;

    [Header("Cameras")]
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _menuCamera;

    public int Score { get; private set; }
    public SaveGameData CurrentSave => _gameSaver.CurrentSave;

    private void Awake()
    {
        Application.targetFrameRate = -1;
        _mainCamera.SetActive(false);
        _menuCamera.SetActive(true);
        _gameSaver.LoadGame();
        _screenController.ShowScreen<MenuScreen>();
        _musicPlayer.PlayMenuMusic();
        _player.DisablePlayer();

        _player.PlayerDeathEvent += OnPlayerDeath;
    }

    private IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(_reloadGameTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator GameOverCor()
    {
        _gameSaver.SaveGame(new SaveGameData
        {
            BestScore = Score > _gameSaver.CurrentSave.BestScore
            ? Score
            : _gameSaver.CurrentSave.BestScore,
            HighestWavesSurvived = (_waveSpawner.CurrentWaveLevel - 1) > _gameSaver.CurrentSave.HighestWavesSurvived
            ? (_waveSpawner.CurrentWaveLevel - 1)
            : _gameSaver.CurrentSave.HighestWavesSurvived,
        });

        yield return new WaitForSeconds(_gameOverTime);
        _screenController.ShowScreen<GameOverScreen>();
    }

    private IEnumerator StartGameCor()
    {
        _mainCamera.SetActive(true);
        _menuCamera.SetActive(false);
        _screenController.ShowScreen<InGameHudScreen>();
        _musicPlayer.PlayMainMusic();
        yield return new WaitForSeconds(_startGameTime);
        _waveSpawner.Activate();
        _player.EnablePlayer();
        _playerAnimationController.OnStartGame();
    }

    private void OnPlayerDeath() => GameOver();

    public void RestartGame() => StartCoroutine(ReloadGame());

    public void GameOver() => StartCoroutine(GameOverCor());

    public void IncrementScore() => Score++;

    public void StartGame()
    {
        StartCoroutine(StartGameCor());
    }

    private void OnDestroy()
    {
        if (_player == null) return;
        
        _player.PlayerDeathEvent -= OnPlayerDeath;
    }
}
