using UnityEngine;

namespace Core
{
    public class Save : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;

        private Storage _storage;
        private GameData _gameData;

        private bool _isSecondStart;

        public void SaveData()
        {
            _storage = new Storage();
            _gameData = new GameData();
            _gameData.FirstUpdateDate(_wallet);

            if (_isSecondStart == false)
            {
                _storage.Save(_gameData);
            }
        }

        public void SetSecondStart()
        {
            _isSecondStart = true;
        }
    }
}

