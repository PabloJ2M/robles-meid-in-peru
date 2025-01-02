using UnityEngine;

namespace Firebase.Auth
{
    public class AuthEditor : AuthBehaviour
    {
        [SerializeField] private string _username, _email;
        [SerializeField, HideInInspector] public string password;

        protected override string AuthID => "EditorAuth";

        protected override void Awake()
        {
            base.Awake();
            if (AuthStatus.isSignIn) return;
            string datos = PlayerPrefs.GetString(AuthID, string.Empty);
            if (!string.IsNullOrEmpty(datos)) { string[] fields = datos.Split(','); _manager.SignIn(fields[0], fields[1]); }
        }

        protected override void OnSignedIn()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(password)) return;

            base.OnSignedIn();
            AuthStatus.Instance.SetLoadingStatus(true);
            PlayerPrefs.SetString(AuthID, $"{_email},{password}");
            _manager.SignIn(_email, password);
        }
        protected override void OnRegister()
        {
            if (string.IsNullOrEmpty(_username)) return;
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(password)) return;
            
            base.OnSignedIn();
            _manager.SetUsername(_username);
            _manager.SignIn(_email, password, _username);
            PlayerPrefs.SetString(AuthID, $"{_email},{password}");
        }
        protected override void OnSignedOut()
        {
            _manager.SignOut();
            PlayerPrefs.SetString(AuthID, string.Empty);
        }
    }
}