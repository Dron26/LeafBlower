using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
public class UIStage : MonoBehaviour
{
    public int number;

    [SerializeField] private ChangerPanel _changerPanel;

        public void OnClickButton()
        {
            _changerPanel.OnClickStages(number);
        }
}
}
