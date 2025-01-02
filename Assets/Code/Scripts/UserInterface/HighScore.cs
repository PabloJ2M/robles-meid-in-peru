using UnityEngine;
using UnityEngine.Events;
using Firebase.Firestore;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private UnityEvent<string> _onShowCurrent;
    [SerializeField] private UnityEvent<string> _onShowHighScore;

    private FirestoreManager _manager;
    private FirestorePlayer _player;

    private void Awake() { _manager = FirestoreManager.Instance; _player = _manager.GetComponent<FirestorePlayer>(); }

    public void DisplayScore()
    {
        var highScore = _player.HighScore;
        _onShowCurrent.Invoke($"{_score.CurrentScore}");

        if (_score.CurrentScore > highScore.Items.integerValue)
        {
            highScore.UpdateHighScore(_score.CurrentScore);
            _manager?.UpdateUserData();
            _player?.OnSaveData();
        }

        _onShowHighScore.Invoke($"Mejor Puntaje<br>{highScore.Items.integerValue}");
    }
}