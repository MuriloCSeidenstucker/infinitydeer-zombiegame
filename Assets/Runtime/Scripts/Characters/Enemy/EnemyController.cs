using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    private int health = 3;

    public int Health { get { return health; } }
    public Transform ThisTransform { get { return transform; } }
    public Vector3 Position { get { return transform.position; } }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
