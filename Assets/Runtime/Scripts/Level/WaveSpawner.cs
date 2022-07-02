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

    private bool preparing;
    private bool hordeActive;

    public PlayerController Player => _player;

    public int EnemiesLeft => _currentWaveEnemies.Count;
    public int CurrentWaveLevel => _currentWaveLevel;
    public int Timer { get { return (int)_timer; } }
    public bool Preparing { get { return preparing; } }
    public bool HordeActive { get { return hordeActive; } }

    private void Awake()
    {
        _enemiesPool.Initialize();
        preparing = true;
        hordeActive = false;
    }

    private void Start()
    {
        _currentWaveValue = _currentWaveLevel * _initialWaveValue;
        _timer = _timeBetweenWaves;
    }

    private void Update()
    {
        if (preparing)
        {
            // não há inimigos

            // timer está ativo
            _timer -= Time.deltaTime;

            // ao final do timer, ativa a horda
            if (Timer <= 0)
            {
                _timer = _timeBetweenWaves;
                preparing = false;

                _currentWaveValue = _currentWaveLevel * _initialWaveValue;
                SpawnEnemies();
                hordeActive = true;
            }
        }

        if (hordeActive)
        {
            // haverá inimgos
            // timer inativo

            // quando não houver inimigos, ativa o em preparação
            if (_currentWaveEnemies.Count == 0)
            {
                _currentWaveLevel++;
                hordeActive = false;
                preparing = true;
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
}
