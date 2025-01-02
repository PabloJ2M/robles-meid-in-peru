using UnityEngine;

namespace Firebase.Firestore.Extra
{
    public class GoalInput : SimpleSingleton<GoalInput>
    {
        private const string saved = "highscore";

        public void AddScore(int value) => PlayerPrefs.SetInt(saved, PlayerPrefs.GetInt(saved) + value);
        //[ContextMenu("Manual Update")] public void UpdateGoal() => StartCoroutine(FirestoreRequest.GetDocument(Goal.path, OnComplete));

        //private void OnComplete(string json)
        //{
        //    if (string.IsNullOrEmpty(json)) return;

        //    FirestoreDocument document = new(json);
        //    GoalData data = new(document);

        //    data.current += PlayerPrefs.GetInt(saved);
        //    document.Update(data.file, new IntField(data.current));

        //    //StartCoroutine(FirestoreRequest.UpdateDocument(Goal.path, string.Empty, document, OnCompleteTransaction));
        //}

        //private void OnCompleteTransaction(string result)
        //{
        //    if (string.IsNullOrEmpty(result)) return;

        //    print("<color=green>updated new score to global</color>");
        //    PlayerPrefs.DeleteKey(saved);
        //}
    }
}