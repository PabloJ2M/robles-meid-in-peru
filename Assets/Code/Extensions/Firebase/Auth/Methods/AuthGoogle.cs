using System.Threading.Tasks;
using UnityEngine;
#if Google_Auth
using Google;
#endif

namespace Firebase.Auth
{
    public class AuthGoogle : AuthBehaviour
    {
        [SerializeField] private string _clientID;
        protected override string AuthID => "GoogleAuth";

        protected override void Awake()
        {
            base.Awake();
            string token = PlayerPrefs.GetString(AuthID);
            if (AuthStatus.isSignIn || string.IsNullOrEmpty(token)) return;
            OnAutoSignIn(token);
        }

        protected override void OnSignedIn()
        {
            if (Application.isEditor) { AuthStatus.Instance.SetResult("Editor is not Allowed"); return; }
            if (_isLoading) return;
            base.OnSignedIn();

#if Google_Auth
            AuthStatus.Instance.SetLoadingStatus(true);
            GoogleSignIn.Configuration = Config();
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(SignInWithGoogle);
#endif
        }
        protected override void OnAutoSignIn(string token)
        {
#if Google_Auth
            base.OnAutoSignIn(token);
            if (!Timeout.IsExpirationDate(AuthID)) { _manager.SignIn(new(token, "google.com")); return; }
            GoogleSignIn.Configuration = Config();
            GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(SignInWithGoogle);
#endif
        }

#if Google_Auth
        private GoogleSignInConfiguration Config() =>
            new() { WebClientId = _clientID, RequestEmail = true, RequestIdToken = true, UseGameSignIn = false };

        private void SignInWithGoogle(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted) { ShowMessage("Google SignIn Error"); return; }
            if (task.IsCanceled) { ShowMessage("Google SignIn Canceled"); return; }

            Credentials credentials = new(task.Result.IdToken, "google.com");
            PlayerPrefs.SetString(AuthID, credentials.tokenID);
            Timeout.SetExpirationDate(AuthID);

            _manager.SetUsername(task.Result.DisplayName);
            _manager.SignIn(credentials);
            SetLoadingStatus(false);

            void ShowMessage(string message)
            {
                GoogleSignIn.DefaultInstance.SignOut();
                AuthStatus.Instance.SetResult(message);
                SetLoadingStatus(false);
            }
        }
#endif
    }
}