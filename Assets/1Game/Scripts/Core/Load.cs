using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class Load : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;

        private Storage _storage;
        private GameData _gameData;

        private bool _isFirstStart;

        public UnityAction LoadParameter;
        public void LoadData()
        {
            _storage = new Storage();
            _gameData = (GameData)_storage.Load(new GameData());
            _wallet.LoadParametr(_gameData.Money, _gameData.MaxMoney);

            LoadParameter?.Invoke();
        }
    }
}