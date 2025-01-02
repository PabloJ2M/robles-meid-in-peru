using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _render;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private ParticleSystem _explode;
    [SerializeField] private UnityEvent _onCollide;

    private ParticleSystem.EmissionModule _emissionModule;

    private void Awake() => _emissionModule = _dust.emission;
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Finish")) return;
        OnDeath();
    }
    public void OnRestart()
    {
        _emissionModule.enabled = _render.enabled = true;
    }
    public void OnDeath()
    {
        _emissionModule.enabled = _render.enabled = false;
        _onCollide.Invoke();
        _explode.Play();
    }
}