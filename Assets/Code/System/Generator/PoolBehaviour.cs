using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public interface IPoolBehaviour
{
    public IObjectPool<IPoolItem> Pool { get; }
    public List<IPoolItem> ActiveItems { get; }
}
public interface IPoolItem
{
    public bool IsActive { set; }
    public GameObject Object { get; }
    public IObjectPool<IPoolItem> Pool { get; set; }
    public Vector3 Position { get; set; }
}

public abstract class PoolBehaviour : MonoBehaviour, IPoolBehaviour
{
    [SerializeField] protected Transform _parent;
    [SerializeField] protected GameObject _prefab;

    private IObjectPool<IPoolItem> _pooling;
    private List<IPoolItem> _activeItems = new();

    public IObjectPool<IPoolItem> Pool => _pooling;
    public List<IPoolItem> ActiveItems => _activeItems;

    protected virtual void Awake() => _pooling = new ObjectPool<IPoolItem>
        (CreateItem, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, true, 20, 100);

    protected virtual IPoolItem CreateItem()
    {
        IPoolItem item = Instantiate(_prefab, _parent).GetComponent<IPoolItem>();
        item.Pool = _pooling;
        return item;
    }

    protected virtual void OnGetFromPool(IPoolItem item) { item.IsActive = true; _activeItems.Add(item); }
    protected virtual void OnReleaseFromPool(IPoolItem item) { item.IsActive = false; _activeItems.Remove(item); }
    protected virtual void OnDestroyPooledObject(IPoolItem item) => Destroy(item.Object);
}