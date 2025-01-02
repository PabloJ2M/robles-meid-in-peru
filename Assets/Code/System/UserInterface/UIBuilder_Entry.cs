using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
    public abstract class UI_Entry<T> : MonoBehaviour, IPoolItem
    {
        public bool IsActive { set => gameObject.SetActive(value); }
        public GameObject Object => gameObject;

        public IObjectPool<IPoolItem> Pool { get; set; }
        public Vector3 Position { get => transform.localPosition; set => transform.localPosition = value; }

        public int GetIndex() => transform.GetSiblingIndex();
        public abstract void Setup(T data);
    }

    public abstract class UI_EntryInteract<T> : UI_Entry<T>, IPointerDownHandler
    {
        protected abstract void OnInteract();
        public void OnPointerDown(PointerEventData eventData) => OnInteract();
    }
}