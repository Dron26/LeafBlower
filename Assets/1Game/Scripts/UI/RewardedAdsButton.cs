using _1Game.Scripts.ADs;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class RewardedAdsButton : MonoBehaviour
    {
        [SerializeField] private AdsSetter _adsSetter;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            gameObject.SetActive(false);
        }
        
        public void OnClick()
        {
            _adsSetter.ShowRewardedAd();
            gameObject.SetActive(false);
        }
    }
    
}