using UnityEngine;

namespace Firebase.Auth
{
    public class AuthManager : Singleton<AuthManager>
    {
        public const string nameID = "username";

        public void SignIn(Credentials credential)
        {
            IAuthType auth = new CredentialsAuth($"id_token={credential.tokenID}&providerId={credential.provider}");
            StartCoroutine(AuthRequest.LoginRequest(AuthMethod.signInWithIdp, auth.Json));
        }
        public void SignIn(string email, string password)
        {
            IAuthType auth = new SimpleAuth(email, password);
            StartCoroutine(AuthRequest.LoginRequest(AuthMethod.signInWithPassword, auth.Json));
        }
        public void SignIn(string email, string password, string username)
        {
            IAuthType auth = new SimpleAuth(email, password, username);
            StartCoroutine(AuthRequest.LoginRequest(AuthMethod.signUp, auth.Json));
        }
        public void SetUsername(string value) => PlayerPrefs.SetString(nameID, value);

        public void SignOut()
        {
            AuthRequest.CurrentUser = new();
            AuthRequest.OnAuthStatusChage?.Invoke(AuthRequest.CurrentUser);
        }

        private void OnEnable() => AuthRequest.OnAuthStatusChage += OnComplete;
        private void OnDisable() => AuthRequest.OnAuthStatusChage -= OnComplete;

        private void OnComplete(User currentUser)
        {
            if (string.IsNullOrEmpty(currentUser.UserID)) return;

            StopAllCoroutines();
            StartCoroutine(AuthRequest.RefreshToken()); //apply refresh token
        }
    }
}