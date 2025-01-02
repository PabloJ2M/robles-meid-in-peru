using UnityEngine;
using UnityEngine.Events;

namespace Toulouse.Events
{
    public class Negate : MonoBehaviour
    {
        [SerializeField] private UnityEvent<bool> _result;

        public void InvertValue(bool value) => _result.Invoke(!value);
    }
}