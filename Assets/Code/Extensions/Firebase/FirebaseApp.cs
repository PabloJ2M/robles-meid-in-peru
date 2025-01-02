using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Firebase
{
    public enum UpsertMethod { POST, GET, PATCH, PUT }

    public static class FirebaseApp
    {
        #region Auth Behavior

        private const string apiKey = "AIzaSyBYtFeYzG-c41XPhhYFY6qbcLZDEViGjd8";
        private const string auth = "https://identitytoolkit.googleapis.com/v1/accounts:";
        private const string refresh = "https://securetoken.googleapis.com/v1/token?key=";

        public static string Auth(string method) => $"{auth}{method}?key={apiKey}";
        public static string Refresh => $"{refresh}{apiKey}";

        #endregion
        #region Firestore Behavior

        private const string proyectID = "touluose-236d6";
        private const string firestore = "https://firestore.googleapis.com/v1/projects";
        public static string Firestore => $"{firestore}/{proyectID}/databases/(default)/documents";

        #endregion

        public static IEnumerator WebRequest(UnityWebRequest request, string idToken, Action<string> result = null)
        {
            if (!string.IsNullOrEmpty(idToken)) request.SetRequestHeader("Authorization", $"Bearer {idToken}");
            yield return WebRequest(request, result);
        }
        public static IEnumerator WebRequest(UnityWebRequest request, Action<string> result = null)
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return SendRequest(request);

            //display error or return result
            bool success = request.result == UnityWebRequest.Result.Success;
            result?.Invoke(success ? request.downloadHandler.text : null);
            if (!success) Debug.LogWarning(request.error);
        }
        public static IEnumerator WebRequest(UnityWebRequest request, Action<long> result = null)
        {
            yield return SendRequest(request);
            result?.Invoke(request.responseCode);
        }

        private static IEnumerator SendRequest(UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
        }
    }
}