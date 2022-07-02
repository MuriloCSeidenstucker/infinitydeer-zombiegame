using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private Transform _enemiesPoolRoot;
    [SerializeField] private Transform[] _respawnPoints;
    //[SerializeField] private EnemyController[] _enemiesPrefabs;
    [SerializeField] private float _timeBetweenWaves = 15.0f;
    [SerializeField] private int _initialWaveValue = 10;
    [SerializeField] private Pool<EnemyController> _enemiesPool;

    private List<EnemyController> _currentWaveEnemies = new List<EnemyController>();
    private int _currentWaveLevel = 1;
    private int _currentWaveValue;
    private float _timer;
    private bool _waveComing = false;
    private bool _waveInProgress = false;

    public PlayerController Player => _player;

    public int EnemiesLeft => _currentWaveEnemies.Count;
    public int CurrentWaveLevel => _currentWaveLevel;
    public int Timer { get { return (int)_timer; } }
    public bool WaveComing { get { return _waveComing; } }
    public bool WaveInProgress { get { return _waveInProgress; } }

    public bool ActivateWaves { get; private set; }

    private void Awake()
    {
        _enemiesPool.Initialize();
        _waveComing = true;
        _waveInProgress = false;
    }

    private void Start()
    {
        _currentWaveValue = _currentWaveLevel * _initialWaveValue;
        _timer = _timeBetweenWaves;
    }

    private void Update()
    {
        if (!ActivateWaves) return;

        if (_waveComing)
        {
            _timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                _timer = _timeBetweenWaves;
                _waveComing = false;

                _currentWaveValue = _currentWaveLevel * _initialWaveValue;
                SpawnEnemies();
                _waveInProgress = true;
            }
        }

        if (_waveInProgress)
        {
            if (_currentWaveEnemies.Count == 0)
            {
                _currentWaveLevel++;
                _waveInProgress = false;
                _waveComing = true;
            }
        }
    }

    private void SpawnEnemies()
    {
        while (_currentWaveValue > 0)
        {
            //EnemyController enemy = _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)];
            Transform randonPoint = _respawnPoints[Random.Range(0, _respawnPoints.Length)];

            EnemyController enemyInstance = _enemiesPool.GetFromPool(randonPoint.position, Quaternion.identity, _enemiesPoolRoot);

            _currentWaveEnemies.Add(enemyInstance);
            _currentWaveValue -= enemyInstance.Cost;
        }
    }

    public void RemoveFromCurrentWave(EnemyController enemy)
    {
        if (_currentWaveEnemies.Contains(enemy))
        {
            _currentWaveEnemies.Remove(enemy);
        }
    }

    public void ReturnToPool(EnemyController enemy)
    {
        _enemiesPool.ReturnToPool(enemy);
    }

    public void IncrementScore() => _gameMode.IncrementScore();
    public void Activate() => ActivateWaves = true;
}
