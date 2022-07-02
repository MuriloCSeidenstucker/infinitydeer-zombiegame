using System;
using UnityEngine;

public class HandleDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private int _defaultHealth = 100;

    private int _currentHealth;

    public event Action DeathEvent;

    public int Layer => gameObject.layer;
    public int Health { get { return _currentHealth; } private set { _currentHealth = value; } }
    public int DefaultHealth { get { return _defaultHealth; } }
    public Vector3 Position { get { return transform.position; } }

    private void OnEnable()
    {
        Health = DefaultHealth;
    }

    private void Die()
    {
        DeathEvent?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0) Die();
    }
}
