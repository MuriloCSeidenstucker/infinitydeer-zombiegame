using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Health { get; }
    Transform ThisTransform { get; }
    Vector3 Position { get; }


    void TakeDamage(int damage);
}
