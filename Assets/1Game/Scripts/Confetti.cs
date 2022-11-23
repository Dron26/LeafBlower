using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Service
{
public class Confetti : MonoBehaviour
{
    [SerializeField] private ParticleSystemController _particleSystem;
        
        private ParticleSystem _particleSystemComponent;

        private void Awake()
        {
            _particleSystemComponent = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
    {
        _particleSystem.CatchAllParticle += OnCatchAllParticle;
    }

    private void OnDisable()
    {
        _particleSystem.CatchAllParticle -= OnCatchAllParticle;
    }

        private void OnCatchAllParticle()
        {
            _particleSystemComponent.Play();
        }
    }
}
