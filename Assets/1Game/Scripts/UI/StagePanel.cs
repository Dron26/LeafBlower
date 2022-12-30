using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class StagePanel : MonoBehaviour
    {
        [SerializeField] private ChangerPanel _changerPanel;

        private Lock _lock;
        private GameObject _stageLock;
        private Button _button;

        private int _number;
        private int _numberGroup;
        private bool _isLocked;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _isLocked = true;
            _lock = GetComponentInChildren<Lock>();
        }

        public void OnClickButton()
        {
            _changerPanel.OnClickStages(_number, _numberGroup);
        }

        public void Initialize(int number, int numberGroup)
        {
            _number = number;
            _numberGroup = numberGroup;

            if (_number == 0)
            {
                _isLocked = false;
            }

            SetLock(_isLocked);
        }

        public void SetLock(bool value)
        {
            _isLocked = value;
            _stageLock = _lock.gameObject;
            _stageLock.SetActive(_isLocked);
            _button.enabled = !_isLocked;
        }
    }
}