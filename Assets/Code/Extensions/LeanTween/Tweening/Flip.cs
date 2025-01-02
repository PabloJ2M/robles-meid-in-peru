using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Toulouse.Effects
{
    public class Flip : Tweening<Vector3>
    {
        [Header("Effect")]
        [SerializeField] private Vector3 _axis;
        [SerializeField, Range(0, 180)] private float _angle;

        public override void Play(bool forward)
        {
            CancelTween();

            Vector3 direction = (forward ? _angle : 0) * _axis;
            LTDescr tween = LeanTween.rotate(gameObject, direction, _time);
            tween.setOnUpdateVector3(OnUpdate);
            tween.setEase(_curve);

            _tweenID = tween.uniqueId;
        }
    }
}