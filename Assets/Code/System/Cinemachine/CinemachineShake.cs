using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _force = 1;
    [SerializeField] private float _speed;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void Awake() => _noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    private void Start() => SetValues(0);
    private void Update() => SetValues(Mathf.MoveTowards(_noise.m_AmplitudeGain, 0, Time.deltaTime * _speed));
    private void SetValues(float value) => _noise.m_AmplitudeGain = _noise.m_FrequencyGain = value;

    public void ShakeCamera() => SetValues(_force);
}