using _1Game.Scripts.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class Load : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;

        private GameData _gameData;

        private bool _isFirstStart;

        public UnityAction LoadParameter;
        public void LoadData()
        {
            _wallet.LoadParametr(_gameData.Money, _gameData.MaxMoney);

            LoadParameter?.Invoke();
        }
    }
}