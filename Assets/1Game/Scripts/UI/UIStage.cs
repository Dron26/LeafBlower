using System;
using System.Collections;
using System.Collections.Generic;
using Empty;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
public class UIStage : MonoBehaviour
{
    public int number;
    private List<Image> _images;
    private int _starCount;
    private StarGroup _starGroup;   
    [SerializeField] private ChangerPanel _changerPanel;


    private void Awake()
    {
        _starGroup = GetComponentInChildren<StarGroup>();
    }

    public void OnClickButton()
        {
            _changerPanel.OnClickStages(number);
        }

        private void SetStars()
        {
            
        }
        
        
}
}
