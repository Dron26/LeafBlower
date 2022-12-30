using _1Game.Scripts.Empty;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class District : MonoBehaviour
    {
        private Lock _lock;
        private GameObject _districkLock;
        private Button _button;

        private bool _isLocked;
        private int _number;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _isLocked = true;
            _lock = GetComponentInChildren<Lock>();
        }

        public void Initialize(int number)
        {
            _number = number;

            if (_number == 0)
            {
                _isLocked = false;
            }

            SetLock(_isLocked);
        }

        public void SetLock(bool value)
        {
            _isLocked = value;
            _districkLock = _lock.gameObject;
            _districkLock.SetActive(_isLocked);
            _button.enabled = !_isLocked;
        }
    }
}