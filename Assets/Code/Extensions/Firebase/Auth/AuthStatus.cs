using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Firebase.Auth
{
    [RequireComponent(typeof(AuthManager))]
    public class AuthStatus : Singleton<AuthStatus>
    {
        public static Action<string> onResult;
        public static Action<bool> onLoading, onSignStatus;
        public static bool isSignIn, isFirstLogin;

        private void OnEnable() => AuthRequest.OnAuthStatusChage += UpdateStatus;
        private void OnDisable() => AuthRequest.OnAuthStatusChage -= UpdateStatus;

        public void SetResult(string value) => onResult?.Invoke(value);

        public async void SetLoadingStatus(bool value)
        {
            if (!value) await Task.Delay(1000);
            else SetResult(string.Empty);
            onLoading?.Invoke(value);
        }
        private void UpdateStatus(User user)
        {
            bool status = !string.IsNullOrEmpty(user.Token);
            onSignStatus?.Invoke(status);
            isSignIn = status;

            if (status) SetResult("<color=green>login</color>");
            else SetResult("<color=yellow>logout</color>");
        }
    }
}