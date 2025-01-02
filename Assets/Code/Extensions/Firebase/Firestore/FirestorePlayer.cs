using UnityEngine;
using Firebase.Auth;

namespace Firebase.Firestore
{
    [RequireComponent(typeof(FirestoreManager))]
    public class FirestorePlayer : MonoBehaviour
    {
        [SerializeField] private UsernameField _username;
        [SerializeField] private HighScoreField _highScore;
        
        public HighScoreField HighScore => _highScore;
        private FirestoreManager _manager;

        #region Application Load & Save Data
        private void Start() { _username.Load(); _highScore.Load(); }
        private void OnApplicationQuit() => OnSaveData();
        private void OnApplicationFocus(bool focus) { if (focus) return; OnSaveData(); }
        public void OnSaveData() { _username.Save(); _highScore.Save(); }
        #endregion
        #region Firestore Callbacks
        private void Awake() => _manager = GetComponent<FirestoreManager>();
        private void OnEnable() { _manager.onReadCallback += ReadCallback; _manager.onWriteCallback += WriteCallback; }
        private void OnDisable() { _manager.onReadCallback -= ReadCallback; _manager.onWriteCallback -= WriteCallback; }
        private void ReadCallback(FirestoreDocument document) => document.GetDocument(_username, _highScore);
        private void WriteCallback(FirestoreDocument document) => document.SetDocument(_username, _highScore);
        #endregion

        public FirestoreDocument DefaultData()
        {
            _username.ReplaceData(new(PlayerPrefs.GetString(AuthManager.nameID)));

            FirestoreDocument document = new();
            document.Update(_username.SaveID, _username.Items);
            document.Update(_highScore.SaveID, _highScore.Items);
            return document;
        }
    }
}