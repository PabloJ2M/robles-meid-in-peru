using Firebase.Auth;
using UnityEngine;

namespace Firebase.Firestore.Extra
{
    public class BaseFirestoreData : MonoBehaviour
    {
        private const string _savedID = "BaseData";
        private string _path, _token;

        private void OnEnable() => AuthStatus.onSignStatus += OnSignedIn;
        private void OnDisable() => AuthStatus.onSignStatus -= OnSignedIn;

        private void OnSignedIn(bool value)
        {
            if (!value) { PlayerPrefs.SetInt(_savedID, 0); return; }
            bool isCompleted = PlayerPrefs.GetInt(_savedID) != 0;
            if (isCompleted) return;

            _path = $"{FirestoreManager.Instance.Collection}/{AuthRequest.CurrentUser.UserID}";
            _token = AuthRequest.CurrentUser.idToken;

            if (string.IsNullOrEmpty(PlayerPrefs.GetString(AuthManager.nameID))) { print("Error: Username not found"); return; }
            StartCoroutine(FirestoreRequest.DocumentExist(_path, DocumentResult));
        }
        private void DocumentResult(bool result)
        {
            if (result) return;
            FirestoreDocument document = FindFirstObjectByType<FirestorePlayer>().DefaultData();
            StartCoroutine(FirestoreRequest.UpdateDocument(_path, _token, document, OnComplete));
        }
        private void OnComplete(string result)
        {
            if (string.IsNullOrEmpty(result)) return;
            PlayerPrefs.SetInt(_savedID, 1);
            print("Updated Name Complete");
        }
    }
}