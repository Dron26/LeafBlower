using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private SettingsPanel _settingPanel;

        float angularVelocity = 5;

        private void Update()
        {
            RectTransform transformParent = GetComponentInParent<RectTransform>();
            transformParent.Rotate(Vector3.forward * Time.deltaTime * angularVelocity);
        }

        public void OnClickButton()
        {
            _settingPanel.gameObject.SetActive(_settingPanel.gameObject.activeInHierarchy != true);
        }
    }
}