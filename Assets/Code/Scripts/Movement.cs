using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 180)] private float _limit;

    private Transform _transform;
    private float _targetVelocity;
    private bool _isPressing;

    private void Awake() => _transform = transform;
    private void OnEnable() => GameManager.Instance.onContinue += OnContinue;
    private void OnDisable() => GameManager.Instance.onContinue -= OnContinue;
    private void OnTouchScreen(InputValue value) => _isPressing = value.isPressed;
    private void OnContinue()
    {
        _transform.rotation = Quaternion.identity;
        _targetVelocity = 0;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isRunning) return;

        _targetVelocity = Mathf.MoveTowards(_targetVelocity, _speed, _isPressing ? 2 : Time.fixedDeltaTime * 50);
        float velocity = _targetVelocity * Time.fixedDeltaTime;

        float angle = _transform.eulerAngles.y;
        angle += _isPressing ? -velocity : velocity;

        angle = ClampAngle(angle, -_limit, _limit);
        _transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    public float ClampAngle(float current, float min, float max)
    {
        float dtAngle = Mathf.Abs(((min - max) + 180) % 360 - 180);
        float hdtAngle = dtAngle * 0.5f;
        float midAngle = min + hdtAngle;

        float offset = Mathf.Abs(Mathf.DeltaAngle(current, midAngle)) - hdtAngle;
        if (offset > 0) current = Mathf.MoveTowardsAngle(current, midAngle, offset);
        return current;
    }
}