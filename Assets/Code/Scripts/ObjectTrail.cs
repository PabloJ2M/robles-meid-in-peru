using System.Collections.Generic;
using UnityEngine;

public class ObjectTrail : SimpleSingleton<ObjectTrail>
{
    [SerializeField] private Transform _defaultParent;
    [SerializeField] private Transform _parent;
    [SerializeField] private HealthUI _healthUI;
    [SerializeField] private Score _score;
    [SerializeField] private float _distance;

    private List<Item> _trail = new();
    private Transform _transform;

    protected override void Awake() { base.Awake(); _transform = transform; }
    private void OnEnable() => GameManager.Instance.onRestart += OnRestart;
    private void OnDisable() => GameManager.Instance.onRestart -= OnRestart;
    private void OnRestart() { for (int i = _trail.Count - 1; i >= 0; i--) _trail[i].ForceRelease(); }

    private void FixedUpdate()
    {
        if (_trail.Count == 0) return;
        float speed = Time.fixedDeltaTime * 10;

        SetPosition(0, _transform.position - _transform.forward * _distance);
        SetRotation(0, _transform.rotation);

        for (int i = 1; i < _trail.Count; i++)
        {
            Item item = _trail[i - 1];
            SetPosition(i, item.Position - item.Fordward * _distance);
            SetRotation(i, item.Rotation);
        }

        void SetPosition(int index, Vector3 target) => _trail[index].Position = Vector3.Lerp(_trail[index].Position, target, speed);
        void SetRotation(int index, Quaternion reference) => _trail[index].Rotation = Quaternion.Lerp(_trail[index].Rotation, reference, speed);
    }

    public void RemoveFromTrail(Item item)
    {
        if (!_trail.Contains(item)) return;
        item.transform.SetParent(_defaultParent);
        _trail.Remove(item);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        Item item = other.GetComponentInParent<Item>();
        if (_trail.Contains(item)) return;
        item.transform.SetParent(_parent);
        _healthUI.AddHealth();
        _score.AddScore(10);
        _trail.Add(item);
    }
}