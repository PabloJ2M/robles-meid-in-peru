using System;
using System.Collections;
using UnityEngine;
using Firebase.Auth;

namespace Firebase.Firestore
{
    public class FirestoreManager : Singleton<FirestoreManager>
    {
        [SerializeField] private string _collection = "Users";
        private string _path;

        public string Collection => _collection;
        public event Action<FirestoreDocument> onReadCallback, onWriteCallback;

        private IEnumerator Start()
        {
            if (!AuthStatus.isSignIn) yield break;
            _path = $"{_collection}/{AuthRequest.CurrentUser.UserID}";

            yield return new WaitUntil(() => AuthStatus.isFirstLogin);
            StartCoroutine(FirestoreRequest.GetDocument(_path, OnUserDataLoaded));
        }
        private void OnUserDataLoaded(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            AuthStatus.isFirstLogin = false;
            onReadCallback?.Invoke(new FirestoreDocument(json));
        }

        [ContextMenu("Update Database")] public void UpdateUserData()
        {
            if (!AuthStatus.isSignIn) return;

            FirestoreDocument document = new();
            onWriteCallback?.Invoke(document);

            if (document.fields.Count == 0) return;
            string token = AuthRequest.CurrentUser.idToken;
            AuthStatus.Instance?.StartCoroutine(FirestoreRequest.UpdateDocument(_path, token, document));
        }
    }
}