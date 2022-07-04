using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyAttack : TriggerDamage
{
    [SerializeField] private float _attackTime = 1f;
    [SerializeField] private AudioClip[] _attackClips;

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
        if (_attackClips.Length > 0)
        {
            int index = Random.Range(0, _attackClips.Length - 1);
            Singleton.Instance.AudioService.PlayAudioCueOneShot(_attackClips[index]);
        }
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
