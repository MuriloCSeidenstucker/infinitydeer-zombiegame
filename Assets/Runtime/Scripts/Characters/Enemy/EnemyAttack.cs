using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttack : TriggerDamage
{
    [SerializeField] private float _attackTime = 1f;

    private Collider _collider;

    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        gameObject.SetActive(false);
        IsAttacking = false;
    }

    public void Attack()
    {
        if (IsAttacking) return;
        
        IsAttacking = true;
    }

    public void WaitForAnimationThenAttack()
    {
        gameObject.SetActive(true);
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(_attackTime);
        gameObject.SetActive(false);
        IsAttacking = false;
    }
}
