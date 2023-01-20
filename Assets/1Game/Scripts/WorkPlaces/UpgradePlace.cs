using _1Game.Scripts.Core;
using _1Game.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class UpgradePlace : MonoBehaviour
    {
        [SerializeField] private UpgradePanel _upgradePanelContainer;
        
        public UnityAction EnterPlace;
        public UnityAction ExitPlace;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                EnterPlace?.Invoke();
                _upgradePanelContainer.gameObject.SetActive(true);
                Debug.Log("SendActionEnter");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                _upgradePanelContainer.gameObject.SetActive(false);
                ExitPlace?.Invoke();
                Debug.Log("SendActionExit");
            }
        }
    }
}