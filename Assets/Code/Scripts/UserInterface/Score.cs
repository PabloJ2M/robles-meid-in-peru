using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private TileSequenceManager _tileSequenceManager;
    [SerializeField] private UnityEvent<string> _onReciveScore;
    [SerializeField] private UnityEvent<string> _onDisplay;
    private float _score;

    public int CurrentScore => (int)_score;

    private void OnEnable() => GameManager.Instance.onRestart += OnRestart;
    private void OnDisable() => GameManager.Instance.onRestart -= OnRestart;
    private void OnRestart() => _score = 0;
    public void AddScore(int value) { _score += value; _onReciveScore.Invoke($"+{value}"); }

    private void Update()
    {
        if (!GameManager.Instance.isRunning) return;
        _score += Time.deltaTime;
        _onDisplay.Invoke(CurrentScore.ToString());
        _tileSequenceManager.SetSequence(CurrentScore);
    }
}