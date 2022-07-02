using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private ScreenController _screenController;
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _reloadGameTime = 1.0f;
    [SerializeField] private float _gameOverTime = 1.0f;

    public int Score { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = -1;

        _player.PlayerDeathEvent += OnPlayerDeath;
    }

    private void OnPlayerDeath() => GameOver();

    private IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(_reloadGameTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(_gameOverTime);
        _screenController.ShowScreen<GameOverScreen>();
    }

    public void RestartGame() => StartCoroutine(ReloadGame());

    public void GameOver() => StartCoroutine(GameOverCor());

    public void IncrementScore() => Score++;

    private void OnDestroy()
    {
        if (_player == null) return;
        
        _player.PlayerDeathEvent -= OnPlayerDeath;
    }
}
