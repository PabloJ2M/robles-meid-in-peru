using System;
using UnityEngine;

namespace Firebase.Firestore
{
    [Serializable]
    public class HighScoreField : Data<IntField>
    {
        [SerializeField] private IntField _highScore;

        public override IntField Items { get => _highScore; protected set => _highScore = value; }
        public override string SaveID => "highscore";

        public void UpdateHighScore(int value)
        {
            _highScore.integerValue = value;
            HasChanged = true;
            OnUpdate();
        }
    }
}