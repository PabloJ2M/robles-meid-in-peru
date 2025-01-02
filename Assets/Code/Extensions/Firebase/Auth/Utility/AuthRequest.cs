using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Firebase.Auth
{
    public enum AuthMethod { signUp, signInWithIdp, signInWithPassword, signInWithIdToken }
    public static class AuthRequest
    {
        public static User CurrentUser;
        public static Action<User> OnAuthStatusChage;
        private const string AuthID = "Firebase";

        public static IEnumerator LoginRequest(AuthMethod method, string json)
        {
            using UnityWebRequest request = new(FirebaseApp.Auth(method.ToString()), UpsertMethod.POST.ToString());

            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            yield return FirebaseApp.WebRequest(request, OnSignInComplete);
        }
        public static IEnumerator RefreshToken()
        {
            DateTime compare = DateTime.Now.AddHours(1);
            Timeout.SetExpirationDate(AuthID);

            WaitForSeconds second = new(1);
            WaitUntil bounds = new(() => DateTime.Now > compare);

            do
            {
                Timeout.IsExpirationDate(AuthID, out compare);
                if (compare.AddMinutes(1) < DateTime.Now) break;
                yield return bounds; yield return second;

                string json = JsonUtility.ToJson(new RefreshTokenRequest(CurrentUser.refreshToken));
                byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

                using UnityWebRequest request = new(FirebaseApp.Refresh, UpsertMethod.POST.ToString());
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);

                yield return FirebaseApp.WebRequest(request, OnRefreshComplete);
                yield return null;
            }
            while (true);

            //reset to default scene
            CurrentUser = new(); SceneManager.LoadScene(0);
        }

        private static void OnSignInComplete(string json)
        {
            AuthStatus.Instance.SetLoadingStatus(false);
            if (string.IsNullOrEmpty(json)) { AuthStatus.Instance.SetResult("Connection Error"); return; }

            CurrentUser = JsonUtility.FromJson<User>(json);
            OnAuthStatusChage?.Invoke(CurrentUser);
        }
        private static void OnRefreshComplete(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            CurrentUser.RefreshToken(JsonUtility.FromJson<Token>(json));
            Timeout.SetExpirationDate(AuthID);
        }
    }
}