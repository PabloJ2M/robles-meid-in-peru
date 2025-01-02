using System;
using UnityEngine;

namespace Firebase.Firestore
{
    [Serializable]
    public class UsernameField : Data<StringField>
    {
        [SerializeField] private StringField _username;

        public override StringField Items { get => _username; protected set => _username = value; }
        public override string SaveID => "name";

        public void UpdateUsername(string value)
        {
            _username.stringValue += value;
            HasChanged = true;
            OnUpdate();
        }
    }
}