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
        private Panel _logoPanel;

   
        private StatsPanel _stats;
        private Panel _statsPanel;
        private SettingsPanel _settings;
        private GameObject _settingsPanel;

        private LanguagePanel _language;
        private GameObject _languagePanel;

        private ExitPanelUI _exitPanel;

        private StagesGroup _stagesGroup;
        private Panel _stagesGroupPanel;

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
            _logoPanel = _logo.gameObject.GetComponent<Panel>();
            _logo.gameObject.SetActive(true);

            _stats = GetComponentInChildren<StatsPanel>();
            _statsPanel = _stats.gameObject.GetComponent<Panel>();

            
            //_statsPanel = _stats.gameObject;
            //_statsPanel.SetActive(false);

            //_locations = GetComponentInChildren<Locations>();
            //_locationsPanel = _locations.gameObject;
            //_locationsPanel.SetActive(false);

            //_smallTown = GetComponentInChildren<SmallTowm>();
            //_smallTownPanel = _smallTown.gameObject;
            //_smallTownPanel.SetActive(false);


            //_settings = GetComponentInChildren<SettingsPanel>();
            //_settingsPanel = _settings.gameObject;
            //_settingsPanel.SetActive(false);

            //_language = GetComponentInChildren<LanguagePanel>();
            //_languagePanel = _language.gameObject;
            //_languagePanel.SetActive(false);

            _exitPanel = GetComponentInChildren<ExitPanelUI>();

            _colorScreen = _screenDim.color;
            _screenDim.raycastTarget = false;

            _stagesGroup = GetComponentInChildren<StagesGroup>();
            _stagesGroupPanel= _stagesGroup.gameObject.GetComponent<Panel>();

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
            _statsPanel.gameObject.SetActive(false);
            StartCoroutine(ChangeColorEnter(_logoPanel));
        }

        public void Exit()
        {
            Application.Quit();
        }

        private IEnumerator ChangeColorExit(Panel activated, Panel activ)
        {
            
            GameObject panelExit= activ.gameObject;

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
            StartCoroutine(ChangeColorEnter(activated));
            StopCoroutine(ChangeColorExit(activated, activ));
        }

        private IEnumerator ChangeColorEnter(Panel activated)
        {
            GameObject panelEnter = activated.gameObject;

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

            StopCoroutine(ChangeColorEnter(activated));
        }

        //public void OnClickStartButton()
        //{
        //    StartCoroutine(ChangeColorExit(_locationsPanel, _logoPanel));
        //}

        //public void OnClickSmallTown()
        //{
        //    StartCoroutine(ChangeColorExit(_smallTownPanel, _locationsPanel));
        //}

        //public void OnClickSmallTownStage(int number)
        //{
        //    SelectSmallTownStage?.Invoke(number);
        //    _isExitStartMenu = true;
        //    StartCoroutine(ChangeColorExit(_statsPanel, _smallTownPanel));
        //}

        public void ClikNextPanel(GameObject panel)
        {
            Panel next = panel.GetComponent<Panel>().next;
            Panel activ = panel.GetComponent<Panel>();
            StartCoroutine(ChangeColorExit(next, activ));
        }

        public void OnClickStages(int number)
        {
            SelectSmallTownStage?.Invoke(number);
            _isExitStartMenu = true;

            StartCoroutine(ChangeColorExit(_statsPanel, _stagesGroupPanel));
        }

        //public void OnClickSettings()
        //{
        //    _settingsPanel.SetActive(true);
        //}

        //public void OnClickLanguage()
        //{
        //    _languagePanel.SetActive(true);
        //}

        public void OnClickBackPanel(GameObject panel)
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
            
        }

        private void OnSetNextLevel()
        {
            _isExitStartMenu = false;
            //StartCoroutine(ChangeColorExit(_locationsPanel,_statsPanel));
        }
    }
}
