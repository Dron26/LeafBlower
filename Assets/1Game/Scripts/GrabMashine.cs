using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabMashine : MonoBehaviour
{
    [SerializeField]  private ParticleSystemController _particleSystem;
    [SerializeField]  private TrashBagIdle _trashBagIdle;

    private Vector3 _velosityParticle;
    private Vector3 _trashBagStartSize;
    private Vector3 _tempTrashBagStartSize;
    private float _maxSize;
    private float _stepUpSize;
    private float _stepSizeDown;
    private float _minSizeParticle;
    //private List <ParticleSystem.Particle> _particle;
    private int _quantityCathcedParticle;
    private bool _isWork;
    private int _quantityUpSize;
    private int _tempQuantityUpSize;
    private int _quantityStepUp;
    private int _quantityAllStepUp;



    public UnityAction<bool> StartFillng;

    private void Start()
    {
        _trashBagStartSize = _trashBagIdle.transform.localScale;
        _tempTrashBagStartSize = _trashBagStartSize;
        _maxSize = 1.13f;
        _stepUpSize = 0.1f;
           _quantityUpSize = 50;
        _tempQuantityUpSize = _quantityUpSize;
        _quantityStepUp = 4;
        StartCoroutine(CountParticle());
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


    private IEnumerator ChangeSize()
    {
        while (_trashBagStartSize.x<)
        {

        }
        _trashBagStartSize
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
