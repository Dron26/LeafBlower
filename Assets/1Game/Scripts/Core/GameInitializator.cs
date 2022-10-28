using System.IO;
using UnityEngine;

namespace Core
{
    public class GameInitializator : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Save _save;

        private string _filePath;

        private void Start()
        {
            var directory = Application.persistentDataPath + "/saves";
            _filePath = directory + "/GameSave.save";

            if (!File.Exists(_filePath))
            {
                Initialize();
            }
            else
            {
                _save.SetSecondStart();
            }
        }

        private void Initialize()
        {
            InitializeWallet();
        }

        private void InitializeWallet()
        {
            int amountMoney = 5;
            int maxAmountMoney = 1000;
            _wallet.Initialization(amountMoney, maxAmountMoney);
        }

    }
}