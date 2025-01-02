using UnityEngine;
using UnityEngine.Events;

namespace Toulouse.Effects
{
    [RequireComponent(typeof(RectTransform))]
    public class Swipe : Tweening<Vector2>
    {
        [Header("Movement")]
        [SerializeField, Tooltip("0 is null")] private float _overrideDistance = 0;
        [SerializeField] private Orientation _orientation;
        [SerializeField] private Direction _direction;
        [SerializeField] private Vector2 _default;
        [SerializeField] private UnityEvent<float> _onValueChange;

        private RectTransform _rect;
        private Vector2 _movement, _position, _target;
        private bool _status;

        protected void Awake() => _rect = GetComponent<RectTransform>();
        private void OnEnable() => onUpdate += Performe;
        private void OnDisable() => onUpdate -= Performe;
        private void Performe(Vector2 position) => _onValueChange.Invoke(MathExtension.InverseLerp(_position, _target, position));

        protected override void Start()
        {
            float size = _orientation == Orientation.Horizontal ? _rect.sizeDelta.x : _rect.sizeDelta.y;
            if (_overrideDistance != 0) size = _overrideDistance;

            _movement = (Vector3)_direction.GetDirection();
            _position = _rect.localPosition;
            _target = (size * _movement) + _position;
            base.Start();
        }
        public override void Play(bool value)
        {
            CancelTween();

            LTDescr tween = LeanTween.moveLocal(_rect.gameObject, value ? _target : _position, _time);
            if (_ignoreTimeScale) tween.setIgnoreTimeScale(true);
            tween.setOnUpdateVector2(OnUpdate);
            tween.setOnComplete(OnComplete);
            tween.setDelay(_delay);
            tween.setEase(_curve);

            _status = value;
            _tweenID = tween.uniqueId;
        }

        public void Switch() => Play(!_status);
        public void SwipeIn() => Play(true);
        public void SwipeOut() => Play(false);
        public void SetDefault() => _rect.localPosition = _default;

        [ContextMenu("SavePosition")]
        public void SaveDefault() => _default = GetComponent<RectTransform>().localPosition;
    }
}