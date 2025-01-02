using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class RootTile : MonoBehaviour, IPoolItem
{
    [SerializeField] private Transform _connection;

    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Vector3 Connection => _connection.position;
    public IObjectPool<IPoolItem> Pool { get; set; }

    public GameObject Object => gameObject;
    public bool IsActive { set => gameObject.SetActive(value); }

    private void LateUpdate() { if (transform.position.z < -50) Pool.Release(this); }
}