using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Firebase.Firestore.Extra
{
    public class Leaderboard : UI_Builder<FirestoreDocument>
    {
        [SerializeField] private string _orderBy = "score";
        [SerializeField] private int _limit = 10;
        [SerializeField] private UnityEvent<bool> _isLoading;

        private string _scopes;

        protected override void Awake() { base.Awake(); _scopes = $"?orderBy={_orderBy} desc&pageSize={_limit}"; }
        protected override void OnDisplay()
        {
            _isLoading.Invoke(true);
            string collection = FirestoreManager.Instance.Collection;
            StartCoroutine(FirestoreRequest.GetDocument($"{collection}/{_scopes}", OnComplete));
        }

        private void OnComplete(string json)
        {
            if (string.IsNullOrEmpty(json)) return;

            //show leaderboard entry's
            _data = new FirestoreSnapshot(json).documents;
            for (int i = 0; i < _data.Count; i++) Pool.Get();
            _isLoading.Invoke(false);

            //hide leaderboard exceed data
            //OnRemoveExceed(_data.Count + 1);
        }
    }
}