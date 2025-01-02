using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Toulouse.UI;

namespace UnityEngine.SceneManagement
{
    public enum Status { Normal, Gameplay }

    [AddComponentMenu("System/SceneManagement/SceneController")]
    public class SceneController : SimpleSingleton<SceneController>
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private FadeScene _fade;
        [SerializeField] private List<string> _ignoreScenes;
        [SerializeField] private UnityEvent<bool> _onGameStatus, _loading;

        public List<string> scenes { get; protected set; }
        public Status status { get; protected set; }
        public Action onSwitchScene;
        private bool _lock;

        protected override void Awake() { base.Awake(); scenes = new(); }
        public void OpenURL(string url) => Application.OpenURL(url);
        public void CutScene(string value) => SceneManager.LoadScene(value);
        public void SwipeScene(string value) => OnFading(value);
        public void Quit() => OnFading(string.Empty);

        public IEnumerator AddScene(string value)
        {
            if (scenes.Contains(value)) yield break; print(value);
            _loading.Invoke(true);

            yield return SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
            if (!_ignoreScenes.Contains(value)) { status = Status.Gameplay; _onGameStatus.Invoke(false); }
            _loading.Invoke(false);
            scenes.Add(value);
        }
        public void RemoveScene(string value)
        {
            if (!scenes.Contains(value)) return;
            if (!_ignoreScenes.Contains(value)) { status = Status.Normal; _onGameStatus.Invoke(true); }
            SceneManager.UnloadSceneAsync(value, UnloadSceneOptions.None);
            scenes.Remove(value);
        }
        private void OnFading(string value)
        {
            if (_lock) return; else _lock = true; onSwitchScene?.Invoke();
            FadeScene fade = Instantiate(_fade, transform);
            fade.onComplete += onComplete;
            fade.onUpdate += onUpdate;

            void onUpdate(float value) { if (_source) _source.volume = 1 - value; }
            void onComplete()
            {
                if (string.IsNullOrEmpty(value)) Application.Quit();
                else SceneManager.LoadSceneAsync(value, LoadSceneMode.Single);
            }
        }
    }
}