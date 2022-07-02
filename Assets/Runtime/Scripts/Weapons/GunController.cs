using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _muzzle;
    [SerializeField] private BulletTrail _bulletTrail;
    [SerializeField] private float _rateOfFire = 6.0f;
    [SerializeField] private int _gunDamage = 50;

    private AutoAim _autoAim;
    private float _fireCooldown = 0f;

    private void Awake()
    {
        _autoAim = GetComponent<AutoAim>();
    }

    private void Update()
    {
        _fireCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (_fireCooldown > 0f) return;

        if (!_autoAim.TryGetTarget(out var nearTarget)) return;

        _fireCooldown = 1f / _rateOfFire;

        //TODO: Implement ObjectPool.
        var trail = Instantiate(_bulletTrail, _muzzle.position, transform.rotation);
        trail.SetTargetPosition(nearTarget.Position);
        nearTarget.TakeDamage(_gunDamage);
    }
}
