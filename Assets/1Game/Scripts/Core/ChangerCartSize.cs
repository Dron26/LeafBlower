using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class ChangerCartSize : MonoBehaviour
    {
        private UpgradeParametrs _upgradeParametrs;
        private GameObject _cart;
        private CartPlane _cartPlane;
        private int _maxRowSize = 2;
        private int _rowSize = 0;
        
        private void Awake()
        {
            _upgradeParametrs = GetComponentInParent<UpgradeParametrs>();
            _cartPlane = GetComponentInChildren<CartPlane>();
            _cart = _cartPlane.gameObject;
        }
        
        private void OnEnable()
        {
            _upgradeParametrs.UpCart += OnUpLevel;
        }

        private void OnDisable()
        {
            _upgradeParametrs.UpCart -= OnUpLevel;
        }
        
        private void OnUpLevel(int valume)
        {
            _rowSize++;
            
            if (_rowSize==_maxRowSize)
            {
                ChangeSize();
            }
        }

        private void ChangeSize()
        {
            float sizeUp = 0.03f;
            Vector3 cartLocalScale = _cart.transform.localScale;
            cartLocalScale= new Vector3(cartLocalScale.x,cartLocalScale.y,cartLocalScale.z+sizeUp);
            _cart.transform.localScale=cartLocalScale;
            _rowSize = 0;
        }
    }
}
//0.4437089  0.6829911