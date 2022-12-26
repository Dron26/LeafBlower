using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class SettingButton : MonoBehaviour
{
    [SerializeField] private SettingsPanel _settingPanel;

    float angularVelocity = 5;
    
    private void Update()
    {
        RectTransform  transformParent = GetComponentInParent<RectTransform>();
        transformParent.Rotate(Vector3.forward*Time.deltaTime*angularVelocity);
    }

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
