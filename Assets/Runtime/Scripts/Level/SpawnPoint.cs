using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 5.0f;

    public Vector3 GeneratePosition() => transform.position + (Random.insideUnitSphere * _spawnRadius).WithAxis(Axis.Y, 0f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
