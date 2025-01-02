using UnityEngine;
using UnityEngine.UI;

namespace Toulouse.System.Ads
{
    public class AdRewardListener : MonoBehaviour
    {
        [SerializeField] private AdBehaviour _manager;
        [SerializeField] private Selectable _adButton;

        private void Awake() => _manager.onCompleted += DisableButton;
        private void OnDestroy() => _manager.onCompleted -= DisableButton;

        public void EnableButton() => _adButton.interactable = true;
        private void DisableButton() => _adButton.interactable = false;
    }
}