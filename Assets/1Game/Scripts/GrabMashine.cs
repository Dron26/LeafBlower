using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabMashine : MonoBehaviour
{
    [SerializeField]  private ParticleSystemController _particleSystem;
    [SerializeField]  private TrashBag _trashBag;
    private GrabMashine _grabMashine;
     private TrashBagIdle _trashBagIdle;
     
    private Vector3 _trashBagStartSize;
    private Vector3 _trashBagStartPosition;
    private Vector3 _tempSize;

    private float _maxSize;
    private float _stepUpSize;
    private float _maxStepUp;
    private float _stepSizeDown;
    private float _minSizeParticle;

    //private List <ParticleSystem.Particle> _particle;
    private int _quantityCathcedParticle;
    private bool _isWork;
    private int _quantityUpSize;
    private int _tempQuantityUpSize;

    private int _quantityStepUp;
    private int _tempQuantityStepUp;

    private int _quantityAllStepUp;

    private WaitForSeconds _waitForSeconds;



    public UnityAction<bool> StartFillng;

    private void Start()
    {
        _grabMashine = GetComponentInParent<GrabMashine>();
        float waiteTime=0.2f;
        _waitForSeconds = new WaitForSeconds(waiteTime);
        _trashBagIdle = GetComponentInChildren<TrashBagIdle>();
        _trashBagStartSize = _trashBagIdle.transform.localScale;
        _trashBagStartPosition= _trashBagIdle.transform.position;
        _tempSize = _trashBagStartSize;
        _maxSize = 1.13f;
        _maxStepUp=0.2f;
        _stepUpSize = 0.05f;
           _quantityUpSize = 50;
        _tempQuantityUpSize = _quantityUpSize;
        _quantityStepUp = 4;
        StartCoroutine(CountParticle());
    }

   private IEnumerator ChangeSize()
    {
        
        while (_tempSize.x <= _trashBagStartSize.x+ _maxStepUp)
        {
            _trashBagIdle.transform.localScale = new Vector3 (_tempSize.x + _stepUpSize, _tempSize.y + _stepUpSize, _tempSize.z + _stepUpSize);
            _tempSize = _trashBagIdle.transform.localScale;
            yield return _waitForSeconds;
        }

        _tempQuantityStepUp++;
        _quantityAllStepUp--;

        if (_tempQuantityStepUp==_quantityStepUp )
        {
            _tempQuantityStepUp = 0;
            CreateTrashBag();
        }
        else
        {
            _quantityStepUp++;
        }
       

    }

    private void CreateTrashBag()
    {
        TrashBag _newTrashBag = Instantiate(_trashBag, _trashBagStartPosition, Quaternion.identity);
        _newTrashBag.transform.SetParent()
    }




    private IEnumerator CountParticle()
    {
        while (_isWork == true)
        {
            if (_quantityAllStepUp>1)
            {
                StartCoroutine(ChangeSize());
            }
        }
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    
    


    public void OnGetParticle()
    {
        _quantityCathcedParticle++;

        if (_quantityCathcedParticle == _tempQuantityUpSize)
        {
            StartFillng?.Invoke(true);
            _quantityAllStepUp++;
            _tempQuantityUpSize += _quantityUpSize;
        }
    }
}
