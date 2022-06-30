using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private float _laserSightLength = 100.0f;
    
    private LineRenderer _lineRenderer;
    private Vector3 _currentLength;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _currentLength = new Vector3(0f, 0f, _laserSightLength);
        _lineRenderer.SetPosition(1, _currentLength);
        _impactEffect.SetActive(false);
    }

    private void FixedUpdate()
    {
        UpdateLaserSightLength();
    }

    private void UpdateLaserSightLength()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _laserSightLength))
        {
            if (hitInfo.collider != null)
            {
                _lineRenderer.SetPosition(1, new Vector3(0f, 0f, hitInfo.distance));
                _impactEffect.transform.position = hitInfo.point;
                _impactEffect.SetActive(true);
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, _currentLength);
            _impactEffect.SetActive(false);
        }
    }
}
