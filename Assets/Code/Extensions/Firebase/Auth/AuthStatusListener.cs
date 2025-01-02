using UnityEngine;
using UnityEngine.Events;

namespace Firebase.Auth
{
    public class AuthStatusListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent<bool> _onLoading;
        [SerializeField] private UnityEvent<string> _onResult;
        [SerializeField] private UnityEvent<bool> _onAuthStatusChange;

        private void Start()
        {
            _onLoading.Invoke(false);
            _onResult.Invoke(string.Empty);
            _onAuthStatusChange.Invoke(AuthStatus.isSignIn);
        }
        private void OnEnable()
        {
            AuthStatus.onResult += _onResult.Invoke;
            AuthStatus.onLoading += _onLoading.Invoke;
            AuthStatus.onSignStatus += _onAuthStatusChange.Invoke;
        }
        private void OnDisable()
        {
            AuthStatus.onResult -= _onResult.Invoke;
            AuthStatus.onLoading -= _onLoading.Invoke;
            AuthStatus.onSignStatus -= _onAuthStatusChange.Invoke;
        }
    }
}