using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class SettingButton : MonoBehaviour
{
    [SerializeField] private SettingsPanel _settingPanel;

    public void OnClickButton()
    {
        if (_settingPanel.gameObject.activeInHierarchy==true)
        {
            _settingPanel.gameObject.SetActive(false);
        }
        else
        {
            _settingPanel.gameObject.SetActive(true);
        }
    }
}
}
