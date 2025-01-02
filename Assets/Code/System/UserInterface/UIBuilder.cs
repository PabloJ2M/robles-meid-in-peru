using System.Collections.Generic;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class UI_Builder<T> : PoolBehaviour
    {
        protected List<T> _data;

        protected virtual void OnEnable() => OnDisplay();
        protected abstract void OnDisplay();

        protected override void OnGetFromPool(IPoolItem item)
        {
            base.OnGetFromPool(item);
            var entry = item as UI_Entry<T>;
            entry.Setup(_data[entry.GetIndex()]);
        }
        protected override void OnReleaseFromPool(IPoolItem item)
        {
            base.OnReleaseFromPool(item);
            item.Object.transform.SetAsLastSibling();
        }
        protected void OnRemoveExceed(int index)
        {
            for (int i = index; i < ActiveItems.Count; i++)
                ActiveItems[i].Pool.Release(ActiveItems[i]);
        }
    }
}