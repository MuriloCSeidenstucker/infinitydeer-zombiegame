using UnityEngine;

public class AutoAim : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _range = 10.0f;
    [SerializeField] private float _angle = 15.0f;

    private IDamageable _currentTarget;
    private Collider[] _enemiesInRange = new Collider[10];
    private Vector3 _toTarget;

    private IDamageable UpdateTarget()
    {
        int collidersCount = Physics.OverlapSphereNonAlloc(transform.position, _range, _enemiesInRange, _enemyLayer);

        float shortestDistance = Mathf.Infinity;
        IDamageable nearestEnemy = null;

        if (collidersCount > 0)
        {
            for (int i = 0; i < collidersCount; i++)
            {
                if (_enemiesInRange[i].TryGetComponent<IDamageable>(out var enemy))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.Position);
                    if (distanceToEnemy < shortestDistance)
                    {
                        shortestDistance = distanceToEnemy;
                        nearestEnemy = enemy;
                    }
                }
            }
        }

        if (nearestEnemy != null && shortestDistance <= _range)
        {
            _currentTarget = nearestEnemy;
        }
        else
        {
            _currentTarget = null;
        }

        return _currentTarget;
    }

    public bool TryGetTarget(out IDamageable target)
    {
        target = UpdateTarget();

        if (target == null) return false;

        if (!FieldOfView.CanSeeTarget(
            radius: _range,
            angle: _angle,
            origin: transform,
            target: target.ThisTransform)) return false;

        return true;
    }

    public Vector3 LookAtTarget(bool isShooting)
    {
        if (_currentTarget != null && isShooting)
        {
            _toTarget = _currentTarget.Position - transform.position;
        }
        else
        {
            _toTarget = Vector3.zero;
        }

        return _toTarget;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || _currentTarget == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);

        var leftDir = Quaternion.Euler(0, -_angle * 0.5f, 0) * transform.forward;
        var rightDir = Quaternion.Euler(0, _angle * 0.5f, 0) * transform.forward;

        Gizmos.color = FieldOfView.CanSeeTarget(radius: _range, angle: _angle, origin: transform, target: _currentTarget.ThisTransform) ? Color.red : Color.white;
        Gizmos.DrawRay(transform.position, leftDir.normalized * _range);
        Gizmos.DrawRay(transform.position, rightDir.normalized * _range);
    }
}
