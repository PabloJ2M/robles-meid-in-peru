using System.Collections;
using UnityEngine;

public class RootSpawner : PoolBehaviour
{
    [SerializeField] private TileSequenceManager _sequenceManager;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private int _maxCount;

    private WaitUntil _spawnDelay;
    public RootTile LastTile { get; private set; }

    protected override void Awake() { base.Awake(); _spawnDelay = new(() => ActiveItems.Count < _maxCount); }
    private void OnEnable() => GameManager.Instance.onContinue += OnContinue;
    private void OnDisable() => GameManager.Instance.onContinue -= OnContinue;
    private void OnContinue() { foreach (var item in ActiveItems) item.Position = new Vector3(0, 0, item.Position.z); }

    protected override IPoolItem CreateItem()
    {
        RootTile tile = Instantiate(_sequenceManager.GetTile(), _parent);
        tile.Pool = Pool;
        return tile;
    }

    private IEnumerator Start()
    {
        yield return _spawnDelay;
        
        RootTile tile = Pool.Get() as RootTile;
        tile.Position = LastTile ? LastTile.Connection : _startPosition;
        LastTile = tile;

        StartCoroutine(Start());
    }
}