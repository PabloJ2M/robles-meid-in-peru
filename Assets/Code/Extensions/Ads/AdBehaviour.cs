using UnityEngine;
using UnityEngine.Events;

namespace Toulouse.System.Ads
{
    public abstract class AdBehaviour : MonoBehaviour
    {
        [SerializeField] protected string[] _url;

        [SerializeField] protected UnityEvent<Texture2D> _onDisplay;
        [SerializeField] protected UnityEvent<string> _onFailure;
        [SerializeField] protected UnityEvent _onSuccess;

        public UnityAction onCompleted { get; set; }
        protected const string _failMessage = "<color=red>failed to load ad</color>";
        protected const string _path = "https://pabloj2m.github.io/video-ads/";
        protected bool _isLoaded;

        protected abstract void Start();
        protected void OnEnable() => _onSuccess.AddListener(OnCompleteHandler);
        protected void OnDisable() => _onSuccess.RemoveAllListeners();
        protected string GetRandomURL() => $"{_path}{_url[Random.Range(0, _url.Length)]}";
        private void OnCompleteHandler() { Time.timeScale = 1; onCompleted?.Invoke(); }

        [ContextMenu("ShowAd")] public virtual void ApplyReward()
        {
            _onFailure.Invoke(string.Empty); if (_isLoaded) return;
            _onFailure.Invoke(_failMessage);
            Start();
        }
    }
}