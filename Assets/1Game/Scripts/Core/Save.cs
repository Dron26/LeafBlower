using UnityEngine;

namespace Core
{
    public class Save : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        private GameData _gameData;

        private bool _isSecondStart;

        public void SaveData()
        {



            if (_isSecondStart == false)
            {
                
            }
        }

        public void SetSecondStart()
        {
            _isSecondStart = true;
        }
    }
}

