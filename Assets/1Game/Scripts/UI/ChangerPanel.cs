using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ChangerPanel : MonoBehaviour
    {
        [SerializeField] private Image _screenDim;
        [SerializeField] private Button _button;
        [SerializeField] private Image _backGround;

        private Button _enterPanelButton;

        private LogoPanel _logo;
        private GameObject _logoPanel;

        private Locations _locations;
        private GameObject _locationsPanel;

        private SmallTowm _smallTown;
        private GameObject _smallTownPanel;

        private StatsPanel _stats;
        private GameObject _statsPanel;


        private SettingsPanel _settings;
        private GameObject _settingsPanel;

        private LanguagePanel _language;
        private GameObject _languagePanel;

        private ExitPanelUI _exitPanel;

        private Color _colorScreen;
        private float _waitTime;

        public UnityAction<int> SelectSmallTownStage;
        public UnityAction<bool> PushAlarm;

        private bool _isExitStartMenu;
        private bool _isPushAlarmWork;
        private float _speedChange = 0.5f;

        private void Awake()
        {
            _logo = GetComponentInChildren<LogoPanel>();
            _logoPanel = _logo.gameObject;
            _logoPanel.SetActive(false);

            _stats = GetComponentInChildren<StatsPanel>();
            _statsPanel = _stats.gameObject;
            _statsPanel.SetActive(false);

            _locations = GetComponentInChildren<Locations>();
            _locationsPanel = _locations.gameObject;
            _locationsPanel.SetActive(false);

            _smallTown = GetComponentInChildren<SmallTowm>();
            _smallTownPanel = _smallTown.gameObject;
            _smallTownPanel.SetActive(false);


            _settings = GetComponentInChildren<SettingsPanel>();
            _settingsPanel = _settings.gameObject;
            _settingsPanel.SetActive(false);

            _language = GetComponentInChildren<LanguagePanel>();
            _languagePanel = _language.gameObject;
            _languagePanel.SetActive(false);

            _exitPanel = GetComponentInChildren<ExitPanelUI>();

            _colorScreen = _screenDim.color;
            _screenDim.raycastTarget = false;

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

        private IEnumerator ChangeColorExit(GameObject panelEnter, GameObject panelExit)
        {
            _screenDim.raycastTarget = true;

            if(_isExitStartMenu == false)
            {
                _backGround.gameObject.SetActive(true);
                _screenDim.gameObject.SetActive(true);
            }

            while (_colorScreen.a < 1)
            {
                _waitTime = Time.fixedDeltaTime* _speedChange;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a += _waitTime;
                _screenDim.color = _colorScreen;
            }

            panelExit.SetActive(false);
            StartCoroutine(ChangeColorEnter(panelEnter));
            StopCoroutine(ChangeColorExit(panelEnter, panelExit));
        }

        private IEnumerator ChangeColorEnter(GameObject panelEnter)
        {          
            panelEnter.SetActive(true);

            if (_isExitStartMenu == true)
            {
                _backGround.gameObject.SetActive(false);
            }
            

            while (_colorScreen.a > 0)
            {
                _waitTime = Time.fixedDeltaTime * _speedChange;
                yield return new WaitForSeconds(_waitTime);
                _colorScreen.a -= _waitTime;
                _screenDim.color = _colorScreen;
            }

            if (_isExitStartMenu == true)
            {
                _screenDim.gameObject.SetActive(false);
            }

            _screenDim.raycastTarget = false;

            StopCoroutine(ChangeColorEnter(panelEnter));
        }

        public void OnClickStartButton()
        {
            StartCoroutine(ChangeColorExit(_locationsPanel, _logoPanel));
        }

        public void OnClickSmallTown()
        {
            StartCoroutine(ChangeColorExit(_smallTownPanel, _locationsPanel));
        }

        public void OnClickSmallTownStage(int number)
        {
            SelectSmallTownStage?.Invoke(number);
            _isExitStartMenu = true;
            StartCoroutine(ChangeColorExit(_statsPanel, _smallTownPanel));
        }

        //public void OnClickSettings()
        //{
        //    _settingsPanel.SetActive(true);
        //}

        //public void OnClickLanguage()
        //{
        //    _languagePanel.SetActive(true);
        //}

        public void OnClickBackSmallTown()
        {
            StartCoroutine(ChangeColorExit(_locationsPanel,_smallTownPanel));
        }

        public void OnClickPushAlarm()
        {
            _isPushAlarmWork = !_isPushAlarmWork;
            PushAlarm?.Invoke(_isPushAlarmWork);
        }
        private void OnDisable()
        {
            
        }

        private void OnSetNextLevel()
        {
            _isExitStartMenu = false;
            StartCoroutine(ChangeColorExit(_locationsPanel,_statsPanel));
        }
    }
}
