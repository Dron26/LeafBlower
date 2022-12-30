using System.Collections;
using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class ChangerPanel : MonoBehaviour
    {
        [SerializeField] private Image _screenDim;
        [SerializeField] private Button _button;
        [SerializeField] private Image _backGround;

        private LogoPanel _logo;
        private Panel _logoPanel;

        private StatsPanel _stats;
        private Panel _statsPanel;
        private ExitPanel _exitPanel;
        private StagesPanel _stagesPanel;
        private GroupStages _groupStages;
        private Panel _stagesGroupPanel;
        private Color _colorScreen;
        
        private float _waitTime;
        private bool _isExitStartMenu;
        private bool _isPushAlarmWork;
        private float _speedChange = 1.2f;

        public UnityAction<int, int> SelectSmallTownStage;
        public UnityAction<bool> PushAlarm;


        private void Awake()
        {
            _logo = GetComponentInChildren<LogoPanel>();
            _logoPanel = _logo.gameObject.GetComponent<Panel>();
            _logo.gameObject.SetActive(true);

            _stats = GetComponentInChildren<StatsPanel>();
            _statsPanel = _stats.gameObject.GetComponent<Panel>();

            _exitPanel = GetComponentInChildren<ExitPanel>();

            _colorScreen = _screenDim.color;
            _screenDim.raycastTarget = false;

            _stagesPanel = GetComponentInChildren<StagesPanel>();
            _stagesGroupPanel = _stagesPanel.gameObject.GetComponent<Panel>();
            _groupStages = _stagesPanel.GetComponentInChildren<GroupStages>();
        }

        private void OnEnable()
        {
            _exitPanel.SetNextLevel += OnSetNextLevel;
        }

        private void Start()
        {
            _isExitStartMenu = false;
            _screenDim.gameObject.SetActive(true);
            _screenDim.raycastTarget = true;
            _backGround.gameObject.SetActive(true);
            _isPushAlarmWork = true;
            StartCoroutine(ChangeColorEnter(_logoPanel));
        }

        public void Exit()
        {
            Application.Quit();
        }

        private IEnumerator ChangeColorExit(Panel activated, Panel active)
        {
            GameObject panelExit = active.gameObject;
            _screenDim.raycastTarget = true;

            if (_isExitStartMenu == false)
            {
                _backGround.gameObject.SetActive(true);
                _screenDim.gameObject.SetActive(true);
            }

            while (_colorScreen.a < 1)
            {
                _waitTime = Time.fixedDeltaTime * _speedChange;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a += _waitTime;
                _screenDim.color = _colorScreen;
            }

            panelExit.SetActive(false);
            StartCoroutine(ChangeColorEnter(activated));
            yield break;
        }

        private IEnumerator ChangeColorEnter(Panel activated)
        {
            activated.gameObject.SetActive(true);

            if (_isExitStartMenu == true)
            {
                _backGround.gameObject.SetActive(false);
            }

            while (_colorScreen.a > 0)
            {
                _waitTime = Time.fixedDeltaTime;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a -= _waitTime * _speedChange;
                _screenDim.color = _colorScreen;
            }

            if (_isExitStartMenu == true)
            {
                _screenDim.gameObject.SetActive(false);
            }

            _screenDim.raycastTarget = false;
            yield break;
        }


        public void ClikNextPanel(GameObject panel)
        {
            Panel next = panel.GetComponent<Panel>().next;
            Panel activ = panel.GetComponent<Panel>();
            StartCoroutine(ChangeColorExit(next, activ));
        }

        public void OnClickStages(int numberStage, int numberGroup)
        {
            SelectSmallTownStage?.Invoke(numberStage, numberGroup);
            _isExitStartMenu = true;

            StartCoroutine(ChangeColorExit(_statsPanel, _stagesGroupPanel));
        }

        public void OnClickBack(GameObject panel)
        {
            Panel next = panel.GetComponent<Panel>().back;
            Panel activ = panel.GetComponent<Panel>();
            StartCoroutine(ChangeColorExit(next, activ));
        }

        public void OnClickPushAlarm()
        {
            _isPushAlarmWork = !_isPushAlarmWork;
            PushAlarm?.Invoke(_isPushAlarmWork);
        }

        private void OnDisable()
        {
            _exitPanel.SetNextLevel -= OnSetNextLevel;
        }

        private void OnSetNextLevel()
        {
            _isExitStartMenu = false;
            StartCoroutine(ChangeColorExit(_stagesGroupPanel, _statsPanel));
            _groupStages.SetStars();
        }
    }
}