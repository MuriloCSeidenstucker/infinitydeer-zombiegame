using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : UIScreen
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private WaveSpawner _waveSpawner;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _lastScoreText;
    [SerializeField] private TextMeshProUGUI _wavesSurvivedText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _highestWavesSurvivedText;

    private void LateUpdate()
    {
        _lastScoreText.text = $"Last Score: {_gameMode.Score}";
        _wavesSurvivedText.text = $"Waves Survived: {_waveSpawner.CurrentWaveLevel}";
    }
}
