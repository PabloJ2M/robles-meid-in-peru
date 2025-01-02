using UnityEngine;

[RequireComponent(typeof(IPoolBehaviour))]
public class PoolMovement : MonoBehaviour
{
    private IPoolBehaviour _pool;
    private PoolManager _manager;

    private void Awake()
    {
        _manager = PoolManager.Instance;
        _pool = GetComponent<IPoolBehaviour>();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isInMenu) return;
        MoveToDirection(-1, _manager.speed * Time.fixedDeltaTime);
    }

    private void MoveToDirection(int normal, float distance)
    {
        if (_pool.ActiveItems.Count == 0) return;

        Vector3 direction = _manager.player.forward * normal * distance;
        direction.y = 0;

        foreach (var track in _pool.ActiveItems)
            track.Position += direction;
    }
}