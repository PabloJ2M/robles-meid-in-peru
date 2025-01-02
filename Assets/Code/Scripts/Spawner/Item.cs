using UnityEngine;
using UnityEngine.Pool;

public class Item : MonoBehaviour, IPoolItem
{
    public Vector3 Fordward => transform.forward;
    public Vector3 Position { get => transform.position; set => transform.position = value; }
    public Quaternion Rotation { get => transform.rotation; set => transform.rotation = value; }

    public bool IsActive { set => gameObject.SetActive(value); }
    public IObjectPool<IPoolItem> Pool { get; set; }
    public GameObject Object => gameObject;

    public void ForceRelease()
    {
        ObjectTrail.Instance.RemoveFromTrail(this);
        Pool.Release(this);
    }
    private void LateUpdate()
    {
        if (Position.z > -15) return;
        ForceRelease();
    }
}