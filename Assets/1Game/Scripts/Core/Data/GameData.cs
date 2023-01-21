
namespace _1Game.Scripts.Core.SaveLoad.Data
{
    [System.Serializable]
    public class GameData
    {
        private Wallet _wallet;
        public long MoneyAmount;
        public long MaxMoneyAmount;


        public GameData( )
        {
            MoneyAmount = _wallet.Money;
            MaxMoneyAmount=_wallet.MaxMoney;
        }
    }
}
