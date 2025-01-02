using System.Threading.Tasks;
using UnityEngine;

public class PoolManager : SimpleSingleton<PoolManager>
{
    [SerializeField] private Health _player;
    [SerializeField] private float _speed;

    public Transform player => _player.transform;
    public float speed => _speed;

    private void OnEnable() => GameManager.Instance.onContinue += Respawn;
    private void OnDisable() => GameManager.Instance.onContinue -= Respawn;
    private async void Respawn()
    {
        await Task.Yield();
        _player.OnRestart();
    }
}