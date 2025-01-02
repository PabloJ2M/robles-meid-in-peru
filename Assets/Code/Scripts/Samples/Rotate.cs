using Unity.Mathematics;
using UnityEngine;

namespace Toulouse.Events
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private Transform _self;
        [SerializeField] private Space _space;

        [Header("Orientation")]
        [SerializeField, Range(0, 10)] private float _speed = 1;
        [SerializeField] private float3 _direction;

        private void FixedUpdate()
        {
            if (_direction.Equals(float3.zero)) return;

            float3 normal = math.normalizesafe(_direction);
            _self?.Rotate(_speed * 10 * Time.fixedDeltaTime * normal, _space);
        }
    }
}