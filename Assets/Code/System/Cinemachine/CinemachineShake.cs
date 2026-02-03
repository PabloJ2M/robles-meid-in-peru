using UnityEngine;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _force = 1;
    [SerializeField] private float _speed;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake() => _noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    private void Start() => SetValues(0);
    private void Update() => SetValues(Mathf.MoveTowards(_noise.AmplitudeGain, 0, Time.deltaTime * _speed));
    private void SetValues(float value) => _noise.AmplitudeGain = _noise.FrequencyGain = value;

    public void ShakeCamera() => SetValues(_force);
}