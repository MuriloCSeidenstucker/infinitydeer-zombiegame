using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHudScreen : UIScreen
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private PlayerController _player;
    [SerializeField] private WaveSpawner _waveSpawner;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _enemiesLeftText;

    [Header("Player Health")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _healthBarImg;
    [SerializeField] private Gradient _gradient;

    private void Start()
    {
        _healthBar.maxValue = _player.DefaultHealth;
    }

    private void LateUpdate()
    {
        _scoreText.text = _gameMode.Score.ToString();
        _currentWaveText.text = $"Wave {_waveSpawner.CurrentWaveLevel}";
        _healthBar.value = _player.Health;
        float percent = (float)_player.Health / _player.DefaultHealth;
        _healthBarImg.color = _gradient.Evaluate(percent);

        if (_waveSpawner.Preparing)
        {
            _timerText.enabled = true;
            _timerText.text = $"Wave coming in {_waveSpawner.Timer} seconds";
            _enemiesLeftText.enabled = false;
        }
        else
        {
            _enemiesLeftText.enabled = true;
            _enemiesLeftText.text = $"{_waveSpawner.EnemiesLeft} enemies left";
            _timerText.enabled = false;
        }
    }
}
