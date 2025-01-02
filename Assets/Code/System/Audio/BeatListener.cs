using UnityEngine;

public class BeatListener : MonoBehaviour
{
    [SerializeField, Range(0, 8)] private int _frequency;
    [SerializeField] private float _sensitivity;

    [SerializeField] private float _downSpeed;
    [SerializeField] private float _speed;

    private BeatDetector _detector;
    private Transform _transform;
    private Vector3 _size;
    private float _target;

    private void Awake() => _detector = GetComponentInParent<BeatDetector>();
    private void Start() { _transform = transform; _size = _transform.localScale; }
    private void OnEnable() => _detector.onUpdate += Performe;
    private void OnDisable() => _detector.onUpdate -= Performe;

    [ContextMenu("Frequency")] private void SetRandomFrequency() => _frequency = Random.Range(0, 8);
    [ContextMenu("Sensitivity")] private void SetRandomSensitivity() => _sensitivity = Random.Range(0f, 1f);

    private void Update()
    {
        _target -= Time.deltaTime * _downSpeed;
        _target = Mathf.Clamp(_target, _size.y, _size.y + 0.5f);
        _transform.localScale = Vector3.Lerp(_transform.localScale, new Vector3(_size.x, _target, _size.z), Time.deltaTime * _speed);
    }
    private void Performe(float[] current, float[] previus)
    {
        if (current[_frequency] < previus[_frequency] * _sensitivity) return;
        _target += 0.1f;
    }
}