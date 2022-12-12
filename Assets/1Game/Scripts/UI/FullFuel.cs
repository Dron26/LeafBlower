using System;
using Service;
using UnityEngine;

public class FullFuel : MonoBehaviour
{
    [SerializeField] private FuelChanger _fuelChanger;

    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    private void OnEnable()
    {
        _fuelChanger.ReachedMaxLevel+=OnReachedMaxLevel;
    }

    private void OnDisable()
    {
        _fuelChanger.ReachedMaxLevel-=OnReachedMaxLevel;
    }

    private void OnReachedMaxLevel()
    {
        _particleSystem.Play();
    }
    
}
