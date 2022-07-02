using System;
using UnityEngine;

public interface IDamageable
{
    event Action DeathEvent;

    int Layer { get; }
    int Health { get; }
    Vector3 Position { get; }

    void TakeDamage(int damage);
}
