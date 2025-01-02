using UnityEngine;
using UnityEngine.Events;

public class HealthUI : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _speed;
    [SerializeField] private UnityEvent<float> _onHealthChange;
    [SerializeField] private UnityEvent _onDeath;
    private float _current = 1;

    private void OnEnable() => GameManager.Instance.onContinue += AddHealth;
    private void OnDisable() => GameManager.Instance.onContinue -= AddHealth;
    public void AddHealth() => _current = 1f;

    private void FixedUpdate()
    {
        if (!GameManager.Instance.isRunning) return;
        _current -= Time.fixedDeltaTime * _speed;
        _current = Mathf.Clamp01(_current);
        _onHealthChange.Invoke(_current);

        if (_current != 0) return;
        _onDeath.Invoke();
    }
}