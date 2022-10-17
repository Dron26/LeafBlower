using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabMashine : MonoBehaviour
{
    [SerializeField]  private ParticleSystemController _particleSystem;

    private Vector3 _velosityParticle;
    private float _stepSizeDown;
    private float _minSizeParticle;
    //private List <ParticleSystem.Particle> _particle;
    private int _quantityCathcedParticle;
    private bool _isWork;
    private int _quantityUpSize;
    private int _quantityStepUp;

    public UnityAction StartFillng;

    private void Start()
    {
        _quantityUpSize = 50;
        _quantityStepUp = 4;
        StartCoroutine(CountParticle());
    }


    private IEnumerator CountParticle()
    {
        while (_isWork == true)
        {
            if (_quantityCathcedParticle == _quantityUpSize)
            {
                StartFillng?.Invoke();

            }
        }
    }
    private void OnEnable()
    {
        _particleSystem.CatchParticle+=
    }

    private void OnDisable()
    {
        
    }


    private IEnumerator ChangeSize()
    {

    }
    


    public void GetParticle(ParticleSystem.Particle particle)
    {
        _quantityCathcedParticle++;

    }
}
