using UnityEngine;
using Toulouse.Effects;

namespace Toulouse.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeCanvas : Fade
    {
        [Header("Canvas Group")]
        [SerializeField] private bool _affectRaycast;
        [SerializeField] private bool _affectInteraction;

        [HideInInspector, SerializeField] private CanvasGroup _group;
        protected override float _alpha { get => _group.alpha; set => _group.alpha = value; }
        public float _default;

        private void Awake() => _group = GetComponent<CanvasGroup>();
        private void OnEnable() => _onFade += OnFadeComplete;
        private void OnDisable() => _onFade -= OnFadeComplete;

        public void Default()
        {
            _alpha = _default;
            OnFadeComplete(true);
        }

        [ContextMenu("SaveAlpha")]
        public void SaveDefault() => _default = GetComponent<CanvasGroup>().alpha;

        private void OnFadeComplete(bool value)
        {
            if (_affectInteraction) _group.interactable = value;
            if (_affectRaycast) _group.blocksRaycasts = value;
        }
    }
}