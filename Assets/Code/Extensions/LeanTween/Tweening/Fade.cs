using System;

namespace Toulouse.Effects
{    
    public abstract class Fade : Tweening<float>
    {
        private float _target = -1;

        protected virtual float _alpha { get; set; }
        protected event Action<bool> _onFade;
        protected bool _isVisible;

        public override void Play(bool forward)
        {
            _target = forward ? 1 : 0;
            if (_alpha == _target) return;
            CancelTween();

            LTDescr tween = LeanTween.value(gameObject, _alpha, _target, _time);
            if (_ignoreTimeScale) tween.setIgnoreTimeScale(true);
            tween.setOnComplete(OnComplete);
            tween.setOnUpdate(OnUpdate);
            tween.setDelay(_delay);
            tween.setEase(_curve);

            _tweenID = tween.uniqueId;
            _isVisible = forward;
        }

        protected override void OnUpdate(float value) { base.OnUpdate(value); _alpha = value; }
        protected override void OnComplete() { base.OnComplete(); _onFade?.Invoke(_isVisible); _alpha = _isVisible ? 1 : 0; }

        public void FadeIn() => Play(true);
        public void FadeOut() => Play(false);
    }
}