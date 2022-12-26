using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class District : MonoBehaviour
{
    
    private int _number;
        
    private Lock _lock;
    private bool _isLocked;
    private GameObject _districkLock;
    private Button  _button;
    // Start is called before the first frame update
    private void Awake()
    {
        _button=GetComponent<Button>();
        _isLocked = true;
        _lock = GetComponentInChildren<Lock>();
    }
    
    public void Initialize(int number)
    {
        _number = number;
            
        if (_number==0)
        {
            _isLocked = false;
        }

        SetLock(_isLocked);
        Debug.Log(_number);
    }

    public void SetLock(bool value)
    {
        _isLocked = value;
        _districkLock = _lock.gameObject;

        _districkLock.SetActive(_isLocked);
        _button.enabled = !_isLocked;
    }
}
