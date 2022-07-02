using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private float _timeBetweenStates = 3.0f;
    
    private PlayerController _player;
    private Animator _animator;
    private int _indexShootingLayer;

    private const string c_velocity = "Velocity";
    private const string c_isDead = "IsDead";
    private const string c_menuStates = "MenuStates";
    private const string c_startGame = "StartGame";
    private const string c_shootingLayer = "Shooting Layer";

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();

        _indexShootingLayer = _animator.GetLayerIndex(c_shootingLayer);
        _animator.SetLayerWeight(_indexShootingLayer, 0f);

        _player.PlayerDeathEvent += OnPlayerDeath;
    }

    private void Start()
    {
        StartCoroutine(MenuStatesCor());
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, _player.CurrentVelocity.sqrMagnitude);
    }

    private void OnPlayerDeath()
    {
        _animator.SetTrigger(c_isDead);
    }

    private IEnumerator MenuStatesCor()
    {
        while (!_player.PlayerActivated)
        {
            yield return new WaitForSeconds(_timeBetweenStates);
            int indexState = Random.Range(1, 4);
            _animator.SetInteger(c_menuStates, indexState);
            yield return null;
            _animator.SetInteger(c_menuStates, 0);
        }
    }

    public void OnStartGame()
    {
        _animator.SetLayerWeight(_indexShootingLayer, 1f);
        _animator.SetTrigger(c_startGame);
    }
}
