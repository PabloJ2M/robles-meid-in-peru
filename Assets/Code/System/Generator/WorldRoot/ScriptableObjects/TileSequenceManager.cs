using UnityEngine;

[CreateAssetMenu(fileName = "manager", menuName = "system/root manager", order = 0)]
public class TileSequenceManager : ScriptableObject
{
    [SerializeField] private int _dificulty = 100;
    [SerializeField] private TileSequence _default;
    [SerializeField] private TileSequence[] _sequences;

    private TileSequence _current;

    public RootTile GetTile() => _current ? _current.RandomTile : _default.RandomTile;
    public void SetSequence(int score)
    {
        int level = Mathf.Clamp(score / _dificulty, 0, _sequences.Length);
        _current = _sequences[level];
    }
}