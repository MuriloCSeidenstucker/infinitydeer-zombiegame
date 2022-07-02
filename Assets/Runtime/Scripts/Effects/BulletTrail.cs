using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float _speed = 40.0f;
    
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        _progress += Time.deltaTime * _speed;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition.WithAxis(Axis.Y, value: _startPosition.y);
    }
}
