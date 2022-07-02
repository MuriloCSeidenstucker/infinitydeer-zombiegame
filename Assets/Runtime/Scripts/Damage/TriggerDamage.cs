using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [Min(0)] [SerializeField] private int _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable)
            && damageable.Layer != gameObject.layer)
        {
            damageable.TakeDamage(_damage);
        }
    }
}
