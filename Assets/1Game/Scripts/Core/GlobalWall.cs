using System;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class GlobalWall : MonoBehaviour
    {
        [SerializeField]private DynamicJoystick _dynamicJoystick;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                float angle = 90f;
                Quaternion rotation = character.gameObject.transform.rotation;
                _dynamicJoystick.gameObject.SetActive(false);
                Debug.Log(_dynamicJoystick.enabled);
                character.gameObject.transform.rotation = new Quaternion(rotation.x, angle,rotation.z,0);
               // _dynamicJoystick.gameObject.SetActive(true);
            }
        }
    }
}
