using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;
    private EnemyController _enemyController;

    private const string c_isAttacking = "IsAttacking";
    private const string c_velocity = "Velocity";
    private const string c_isDead = "IsDead";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void LateUpdate()
    {
        _animator.SetFloat(c_velocity, _enemyController.CurrentVelocity.sqrMagnitude);
        _animator.SetBool(c_isAttacking, _enemyController.ZombieAttack.IsAttacking);
    }

    //TODO: Find a better way to deal with it.
    private void AttackAnimEvent()
    {
        _enemyController.ZombieAttack.WaitForAnimationThenAttack();
    }

    public void OnDeath()
    {
        _animator.SetTrigger(c_isDead);
    }
}
