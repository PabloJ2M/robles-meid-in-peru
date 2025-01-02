using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace Toulouse.System.Ads
{
    public class AdImageReward : AdBehaviour
    {
        [Header("Display Time UI")]
        [SerializeField, Range(0, 60)] private float _time;
        [SerializeField] private UnityEvent<float> _onTime;

        private Texture2D _texture;
        private float _remaining;
        private bool _isPlaying;

        private void Awake() => _remaining = _time;
        protected override void Start() { StopAllCoroutines(); StartCoroutine(DownloadImage()); }
        private void Update()
        {
            if (!_isPlaying) return;
            _remaining = Mathf.Clamp(_remaining - Time.unscaledDeltaTime, 0, _time);
            _onTime.Invoke(_remaining / _time);

            if (_remaining != 0) return;
            _isPlaying = false;
            _onSuccess?.Invoke();
        }

        private IEnumerator DownloadImage()
        {
            using UnityWebRequest request = UnityWebRequestTexture.GetTexture(GetRandomURL());
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) yield break;
            _texture = DownloadHandlerTexture.GetContent(request);
            _isLoaded = true;
        }
        public override void ApplyReward()
        {
            base.ApplyReward(); if (!_isLoaded) return;
            _onDisplay.Invoke(_texture); _remaining = _time; _isPlaying = true;
            Time.timeScale = 0;
        }
    }
}