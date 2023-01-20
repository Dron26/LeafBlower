using UnityEngine;

namespace _1Game.Scripts.UI
{
    public class SettingButton : MonoBehaviour
    {
        [SerializeField] private SettingsPanel _settingPanel;

        public void OnClickButton()
        {
            _settingPanel.gameObject.SetActive(_settingPanel.gameObject.activeInHierarchy != true);
        }
    }
}