using System.Text;
using UnityEngine;
#if Apple_Auth
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Native;
using AppleAuth.Interfaces;
#endif

namespace Firebase.Auth
{
    public class AuthApple : AuthBehaviour
    {
        #if Apple_Auth
        private IAppleAuthManager _auth;
        private NonceUtility _nonce;
        #endif

        protected override string AuthID => "AppleAuth";
        private const string _userID = "AppleUserID";
        private string _id = string.Empty;

        #if Apple_Auth
        private void Start() { _id = PlayerPrefs.GetString(_userID); Credentials(_id); }
        private void Update() => _auth?.Update();
        private void Error(IAppleError error) { print(error.LocalizedFailureReason); SetLoadingStatus(false); }
        private void Status(CredentialState state) => print(state);
        private void Credentials(string id) { if (!string.IsNullOrEmpty(id)) _auth?.GetCredentialState(id, Status, Error); }
        #endif

        protected override void Awake()
        {
            base.Awake();

            #if Apple_Auth
            if (!AppleAuthManager.IsCurrentPlatformSupported) return;
            _auth = new AppleAuthManager(new PayloadDeserializer());
            #endif

            string token = PlayerPrefs.GetString(AuthID);
            if (AuthStatus.isSignIn || string.IsNullOrEmpty(token)) return;
            OnAutoSignIn(token);
        }
        protected override void OnSignedIn()
        {
            base.OnSignedIn();
            if (Application.isEditor) { print("Editor is not Allowed"); return; }
            if (_isLoading) return;

            #if Apple_Auth
            _nonce = new NonceUtility(32);
            var options = LoginOptions.IncludeFullName | LoginOptions.IncludeEmail;
            AuthStatus.Instance.SetLoadingStatus(true);
            _auth?.LoginWithAppleId(new AppleAuthLoginArgs(options, _nonce.Nonce), SingInWithIOs, Error);
            #endif
        }
        protected override void OnAutoSignIn(string token)
        {
            #if Apple_Auth
            base.OnAutoSignIn(token);
            if (!Timeout.IsExpirationDate(AuthID)) { _manager.SignIn(new(token, "apple.com")); return; }

            _nonce = new NonceUtility(32);
            _auth.QuickLogin(new AppleAuthQuickLoginArgs(_nonce.Nonce), SingInWithIOs, Error);
            #endif
        }

        #if Apple_Auth
        private void SingInWithIOs(ICredential appCredentials)
        {
            IAppleIDCredential appleID = appCredentials as IAppleIDCredential;
            if (_id != appleID.User) PlayerPrefs.SetString(_userID, appleID.User);

            //save login configuration
            string fullname = appleID.FullName != null ? $"{appleID.FullName.GivenName} {appleID.FullName.FamilyName}" : string.Empty;
            if (!string.IsNullOrEmpty(fullname)) _manager.SetUsername(fullname);

            //get identity & autorization with app ID
            string token = Encoding.UTF8.GetString(appleID.IdentityToken, 0, appleID.IdentityToken.Length);
            Credentials credentials = new(token, $"apple.com&nonce={_nonce.RowNonce}");
            PlayerPrefs.SetString(AuthID, token);
            Timeout.SetExpirationDate(AuthID);

            _manager.SignIn(credentials);
            SetLoadingStatus(false);
        }
        #endif
    }
}