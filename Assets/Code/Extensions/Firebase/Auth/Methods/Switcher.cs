using System;
using UnityEngine;

namespace Firebase.Auth
{
    [Serializable] public struct AuthType
    {
        [SerializeField] private RuntimePlatform _platform;
        [SerializeField] private GameObject _button;

        public void SetActive(RuntimePlatform value) => _button?.SetActive(value.Equals(_platform));
    }

    public class Switcher : MonoBehaviour
    {
        [SerializeField] private GameObject _internet;
        [SerializeField] private AuthType[] _types;

        private void ActivePlatform(RuntimePlatform runtime) { print(runtime); foreach (var item in _types) item.SetActive(runtime); }
        public void SetStatus(bool value) => ActivePlatform(value ? RuntimePlatform.PS5 : Application.platform);
    }
}