using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spawner : PoolBehaviour
{
    [SerializeField] private GameObject[] _randomItems;
    [SerializeField] private RootSpawner _roadGenerator;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layerMask;

    private BoxCollider _collider;
    private Transform _transform;
    private WaitUntil _areaCheck;

    protected override void Awake()
    {
        base.Awake();
        _transform = transform;
        _collider = GetComponent<BoxCollider>();
        Vector3 position = _transform.position + _collider.center;
        _areaCheck = new(() => !Physics.CheckBox(position, _collider.size / 2, default, _layerMask, QueryTriggerInteraction.Ignore));
    }
    private void OnEnable() => GameManager.Instance.onContinue += OnContinue;
    private void OnDisable() => GameManager.Instance.onContinue -= OnContinue;

    private void Update()
    {
        if (!_roadGenerator.LastTile) return;
        Vector3 position = _transform.position;
        position.x = _roadGenerator.LastTile.transform.position.x;
        _transform.position = position;
    }

    private IEnumerator OnBegin()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        yield return _areaCheck;
        Pool.Get();
        StartCoroutine(OnBegin());
    }
    protected override IPoolItem CreateItem()
    {
        IPoolItem item = Instantiate(_randomItems[Random.Range(0, _randomItems.Length)], _parent).GetComponent<IPoolItem>();
        item.Pool = Pool;
        return item;
    }
    protected override void OnGetFromPool(IPoolItem item)
    {
        base.OnGetFromPool(item);

        Vector3 pos = item.Position;
        pos.x = Random.Range(-_distance, _distance);
        pos.z = transform.position.z;
        item.Position = pos;
    }

    public void Stop() => StopAllCoroutines();
    private void OnContinue()
    {
        for (int i = ActiveItems.Count - 1; i >= 0; i--)
        {
            if (ActiveItems[i].Object.transform.IsChildOf(_parent))
                ActiveItems[i].Pool.Release(ActiveItems[i]);
        }

        StartCoroutine(OnBegin());
    }
}