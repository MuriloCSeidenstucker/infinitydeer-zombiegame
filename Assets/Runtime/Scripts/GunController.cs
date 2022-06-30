using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private BulletTrail _bulletTrail;
    [SerializeField] private float rateOfFire = 6.0f;

    private AutoAim _autoAim;
    private float fireCooldown = 0f;

    private void Awake()
    {
        _autoAim = GetComponent<AutoAim>();
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (fireCooldown > 0f) return;

        if (!_autoAim.TryGetTarget(out var nearTarget)) return;

        fireCooldown = 1f / rateOfFire;

        //TODO: Implement ObjectPool.
        var trail = Instantiate(_bulletTrail, _muzzle.position, transform.rotation);
        trail.SetTargetPosition(nearTarget.Position);
        nearTarget.TakeDamage(1);
    }
}
