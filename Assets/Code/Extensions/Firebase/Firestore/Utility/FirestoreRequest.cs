using System;
using System.Text;
using System.Collections;
using UnityEngine.Networking;

namespace Firebase.Firestore
{
    public static class FirestoreRequest
    {
        public static IEnumerator DocumentExist(string path, Action<bool> result = null)
        {
            string url = $"{FirebaseApp.Firestore}/{path}";
            using UnityWebRequest request = new UnityWebRequest(url, UpsertMethod.GET.ToString());
            yield return FirebaseApp.WebRequest(request, (long code) => result?.Invoke(code != 404));
        }

        /// <param name="path">Collection/DocumentID</param>
        public static IEnumerator GetDocument(string path, Action<string> result = null)
        {
            string url = $"{FirebaseApp.Firestore}/{path}";
            using UnityWebRequest request = new UnityWebRequest(url, UpsertMethod.GET.ToString());
            yield return FirebaseApp.WebRequest(request, result);
        }

        /// <param name="path">Collection/DocumentID</param>
        /// <param name="idToken">Authentication Token</param>
        public static IEnumerator UpdateDocument(string path, string idToken, FirestoreDocument document, Action<string> result = null)
        {
            string url = $"{FirebaseApp.Firestore}/{path}{document.Mask()}";
            byte[] bodyRaw = Encoding.UTF8.GetBytes(document.ToJson());

            using UnityWebRequest request = new UnityWebRequest(url, UpsertMethod.PATCH.ToString());
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            yield return FirebaseApp.WebRequest(request, idToken, result);
        }
    }
}