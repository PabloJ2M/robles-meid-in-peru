using System;
using UnityEngine;

namespace Toulouse.Effects
{
    public enum Orientation { Horizontal, Vertical }
    public enum Direction { Top, Right, Left, Bottom }

    [DefaultExecutionOrder(-10)]
    public abstract class Tweening<T> : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] protected AnimationCurve _curve;
        [SerializeField, Range(0, 1)] protected float _time = 1, _delay = 0;
        [SerializeField] protected bool _loop = false, _playOnAwake, _ignoreTimeScale;

        public event Action<T> onUpdate;
        protected int _tweenID = -1;

        protected virtual void Start() { if (_playOnAwake) Play(true); }
        protected virtual void OnDestroy() => CancelTween();
        protected virtual void OnComplete() => _tweenID = -1;
        protected virtual void OnUpdate(T value) => onUpdate?.Invoke(value);

        public abstract void Play(bool value);
        public void CancelTween() { if (_tweenID >= 0) LeanTween.cancel(_tweenID); }
    }
}