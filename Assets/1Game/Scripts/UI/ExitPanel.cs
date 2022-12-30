using _1Game.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.UI
{
    public class ExitPanel : MonoBehaviour
    {
        [SerializeField] private ExitPlace _exitPlaceOnStage;

        private Panel _panel;

        public UnityAction SetNextLevel;
        public UnityAction UpdateData;

        private void Awake()
        {
            _panel = GetComponentInChildren<Panel>();
        }

        private void Start()
        {
            _panel.gameObject.SetActive(false);
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
            _panel.gameObject.SetActive(isEnterPlace);
        }

        public void OnTapBattonYes()
        {
            UpdateData?.Invoke();
            SetNextLevel?.Invoke();
        }
    }
}