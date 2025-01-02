using UnityEngine;

[CreateAssetMenu(fileName = "root", menuName = "system/root", order = 1)]
public class TileSequence : ScriptableObject
{
    [SerializeField] private RootTile[] _middle;

    public RootTile RandomTile => _middle[Random.Range(0, _middle.Length)];
}