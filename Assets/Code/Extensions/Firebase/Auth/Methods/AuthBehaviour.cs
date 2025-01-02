using System;
using UnityEngine;

namespace Firebase.Auth
{
    [Serializable] public struct Visual
    {
        public GameObject icon, loading;
    }

    public abstract class AuthBehaviour : MonoBehaviour
    {
        [SerializeField] private Visual _visual;
        protected AuthManager _manager;
        protected bool _isLoading;

        protected abstract string AuthID { get; }
        protected virtual void Awake() => _manager = AuthManager.Instance;

        public void SignIn() => OnSignedIn();
        public void Register() => OnRegister();
        public void SignOut() => OnSignedOut();

        protected virtual void OnSignedIn() => AuthStatus.isFirstLogin = true;
        protected virtual void OnRegister() => AuthStatus.isFirstLogin = true;
        protected virtual void OnSignedOut() { }
        protected virtual void OnAutoSignIn(string token) => SetLoadingStatus(true);
        protected void SetLoadingStatus(bool isLoading)
        {
            _isLoading = isLoading;
            _visual.icon?.SetActive(!_isLoading);
            _visual.loading?.SetActive(_isLoading);
        }
    }
}