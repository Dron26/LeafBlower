using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIStage : MonoBehaviour
    {
        [SerializeField] private ChangerPanel _changerPanel;

        private int _number;
        private StageLock _stageLock;
        private bool _isLocked;
        private GameObject _lock;
        private Button  _button;

        private void Awake()
        {
            _button=GetComponent<Button>();
            _isLocked = true;
            _stageLock = GetComponentInChildren<StageLock>();
        }

        public void OnClickButton()
        {
            _changerPanel.OnClickStages(_number);
            Debug.Log(_number);
        }

        public void Initialize(int number)
        {
            _number = number;
            
            if (_number==0)
            {
                _isLocked = false;
            }

            SetLock(_isLocked);
        }

        public void SetLock(bool value)
        {
            _isLocked = value;
            _lock = _stageLock.gameObject;

            _lock.SetActive(_isLocked);
            _button.enabled = !_isLocked;
        }
    }
}
