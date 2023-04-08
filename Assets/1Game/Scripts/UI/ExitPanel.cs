using _1Game.Scripts.Core;
using _1Game.Scripts.Core.SaveLoad;
using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class ExitPanel : MonoBehaviour
    {
        [SerializeField] private ExitPlace _exitPlaceOnStage;
        [SerializeField] private StarPanel _starPanel;
        [SerializeField] private SaveLoad _saveLoad;
        
        private Panel _panel;
        private ExitPanelWarning _warningPanel;
        public UnityAction SetNextLevel;
        public UnityAction UpdateData;
        private int _countStars;
        private void Awake()
        {
            _panel = GetComponentInChildren<Panel>();
            _warningPanel = GetComponentInChildren<ExitPanelWarning>();
            _countStars = 3;
        }

        private void Start()
        {
            _panel.gameObject.SetActive(false);
            _warningPanel.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _exitPlaceOnStage.EnterPlace += OnEnterPlace;
        }

        private void OnDisable()
        {
            _exitPlaceOnStage.EnterPlace -= OnEnterPlace;
        }

        private void OnEnterPlace(bool isEnterPlace)
        {
            if (_starPanel.CountStars != _countStars)
            {
                _warningPanel.gameObject.SetActive(true);
            }
            
            _panel.gameObject.SetActive(isEnterPlace);
        }

        public void OnTapBattonYes()
        {
            UpdateData?.Invoke();
            SetNextLevel?.Invoke();
            _saveLoad.Save();
            
        }  
    }      
}