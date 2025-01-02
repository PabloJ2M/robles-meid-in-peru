using UnityEngine;
using UnityEngine.UI;

namespace Firebase.Firestore.Extra
{
    public class ScrollEffect : ScrollRect
    {
        private Vector2 _minSize;

        protected override void Awake()
        {
            base.Awake();
            _minSize = new(0.5f, 0.5f);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (content.childCount == 0) return;
            float delta = Time.deltaTime;
            float height = content.rect.height;

            for (int i = 0; i < content.childCount; i++)
            {
                RectTransform child = content.GetChild(i) as RectTransform;
                if (child == null) continue;

                float point = child.anchoredPosition.y + child.rect.height * 0.5f;
                float target = content.anchoredPosition.y + point;

                child.localScale = Vector2.Lerp(Vector2.one, _minSize, Mathf.Abs(target) / height);
            }
        }
    }
}