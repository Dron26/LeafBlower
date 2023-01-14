namespace _1Game.Scripts.Core
{
    [System.Serializable]
    public class LocalizationDate
    {
        public LocalizationItem[] items;
    }

    [System.Serializable]
    public class LocalizationItem
    {
        public string key;
        public string value;
    }
}