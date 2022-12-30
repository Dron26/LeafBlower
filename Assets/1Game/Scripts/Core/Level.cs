
namespace _1Game.Scripts.Core
{
    public class Level
    {
        public int Value => _value;
        public int Price => _price;

        private int _value { get; set; }
        private int _price { get; set; }

        public Level( int value, int price)
        {
            _value = value;
            _price = price;
        }
    }
}
