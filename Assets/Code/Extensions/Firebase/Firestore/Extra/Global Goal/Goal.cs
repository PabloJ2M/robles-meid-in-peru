//using Toulouse.System;
using UnityEngine;
using UnityEngine.Events;

namespace Firebase.Firestore.Extra
{
    public class Goal : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> _onUpdate;
        [SerializeField] private UnityEvent<string> _onUpdateUI;

        //private PlayerData _playerData;

        //private void Awake() => _playerData = PlayerData.Instance;
        //private void OnEnable() => _playerData.Score.OnItemsUpdated += OnPerforme;
        //private void OnDisable() => _playerData.Score.OnItemsUpdated -= OnPerforme;
        private void OnPerforme(IntField number)
        {

        }

        //protected override void Awake() { base.Awake(); path = $"{_collection}/{_documentID}"; }
        //private void Start() => ManualRefresh();
        //[ContextMenu("Refresh")] public void ManualRefresh() => StartCoroutine(FirestoreRequest.GetDocument(path, OnComplete));

        //private void OnComplete(string json)
        //{
        //    GoalData data;
        //    if (string.IsNullOrEmpty(json)) data = JsonUtility.FromJson<GoalData>(PlayerPrefs.GetString(path));
        //    else { FirestoreDocument document = new(json); data = new(document); PlayerPrefs.SetString(path, JsonUtility.ToJson(data)); }
        //    DisplayData(data);
        //}
        //private void DisplayData(GoalData data)
        //{
        //    _onUpdateUI.Invoke(data.max.ToString());
        //    _onUpdate.Invoke(data.current / (data.max == 0 ? 1 : (float)data.max));
        //}
    }
}