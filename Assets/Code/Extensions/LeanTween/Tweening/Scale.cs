using UnityEngine;

namespace Toulouse.Effects
{
    public class Scale : Tweening<Vector2>
    {
        [Header("Effects")]
        [SerializeField, Range(0f, 1.2f)] private float _factor = 1;

        public override void Play(bool forward)
        {
            CancelTween();

            LTDescr tween = LeanTween.scale(gameObject, forward ? _factor * mathf.one : mathf.one, _time);
            if (_loop) tween.setLoopPingPong(-1);
            tween.setEase(_curve);
            _tweenID = tween.uniqueId;
        }
    }
}