using System;

namespace Toulouse.UI
{
    public class FadeScene : FadeCanvas
    {
        public Action onComplete { get; set; }
        private bool _change;

        protected override void Start()
        {
            _change = onComplete != null;
            _alpha = _change ? 0 : 1;

            if (!_playOnAwake) return;
            if (_change) FadeIn();
            else FadeOut();
        }
        protected override void OnComplete()
        {
            if (!_change) Destroy(gameObject);
            onComplete?.Invoke();
            base.OnComplete();
        }
    }
}