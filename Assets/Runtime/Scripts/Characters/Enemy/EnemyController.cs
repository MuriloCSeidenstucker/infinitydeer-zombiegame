using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour, IPooledObject
{
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private float _attackRange = 2.5f;

    [Header("Wading Parameters")]
    [SerializeField] private int _randomDist = 5;
    [SerializeField] private float _idleTime = 3.0f;

    private CharacterMovement _enemyMovement;
    private WaveSpawner _waveSpawner;
    private PlayerController _player;
    private EnemyAnimationController _enemyAnimationController;
    private Collider _collider;
    private IDamageable _damageable;
    private Vector3 _anywhere = Vector3.zero;
    Vector3 _toAnywhere = Vector3.zero;
    private bool _isAggressive = false;
    private bool _wadingStarted = false;
    private bool _isDead = false;

    public EnemyAttack ZombieAttack { get { return _enemyAttack; } }
    public Vector3 CurrentVelocity => _enemyMovement.CurrentVelocity;
    public int Cost { get; private set; } = 2;
    public bool IsDead { get { return _isDead; } }

    private void Awake()
    {
        _waveSpawner = GetComponentInParent<WaveSpawner>();
        _enemyMovement = GetComponent<CharacterMovement>();
        _damageable = GetComponent<IDamageable>();
        _enemyAnimationController = GetComponentInChildren<EnemyAnimationController>();
        _collider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (!_wadingStarted && !_isAggressive)
        {
            _wadingStarted = true;
            StartCoroutine(Wading());
        }

        if (_isAggressive)
        {
            Hunting();
        }
    }

    private void Hunting()
    {
        Vector3 toPlayer = _player.transform.position - transform.position;
        _enemyMovement.ProcessRotationInput(toPlayer);

        if (Vector3.Distance(_player.transform.position, transform.position) <= _attackRange)
        {
            _enemyMovement.StopImmediately();
            ZombieAttack.Attack();
        }
        else if (!ZombieAttack.IsAttacking)
        {
            _enemyMovement.ProcessMovementInput(toPlayer);
        }
    }

    private IEnumerator Wading()
    {
        while (true)
        {
            if (_anywhere == Vector3.zero)
            {
                yield return new WaitForSeconds(_idleTime);
                _anywhere = transform.position + Random.insideUnitSphere * _randomDist;
                _anywhere.y = 0f;
                _toAnywhere = _anywhere - transform.position;
            }

            if (Vector3.Distance(transform.position, _anywhere) > 0.5f)
            {
                _enemyMovement.ProcessMovementInput(_toAnywhere);
                _enemyMovement.ProcessRotationInput(_toAnywhere);
                yield return null;
            }
            else
            {
                _enemyMovement.StopImmediately();
                _anywhere = Vector3.zero;
                yield return null;
            }
        }
    }

    private void OnDeath()
    {
        if (!IsDead)
        {
            _enemyAnimationController.OnDeath();
            StartCoroutine(DeathCor());
        }
    }

    private IEnumerator DeathCor()
    {
        _isDead = true;
        _enemyMovement.StopImmediately();
        _collider.enabled = false;
        enabled = false;
        _waveSpawner.IncrementScore();
        _waveSpawner.RemoveFromCurrentWave(this);
        yield return new WaitForSeconds(2f);
        _waveSpawner.ReturnToPool(this);
    }

    private void OnPlayerDeath()
    {
        _enemyMovement.StopImmediately();
        _isAggressive = false;
    }

    public void Initialize()
    {
        enabled = true;
        _player = _waveSpawner.Player;
        _isAggressive = true;
        _isDead = false;
        _collider.enabled = true;
    }

    public void OnInstantiated()
    {
        Initialize();
    }

    public void OnEnabledFromPool()
    {
        Initialize();
        Assert.IsNotNull(_damageable, $"_damageable interface was not serialized");
        _damageable.DeathEvent += OnDeath;
        _player.PlayerDeathEvent += OnPlayerDeath;
    }

    public void OnDisabledFromPool()
    {
        if (_damageable == null) return;
        _damageable.DeathEvent -= OnDeath;

        if (_player == null) return;
        _player.PlayerDeathEvent -= OnPlayerDeath;
    }
}
