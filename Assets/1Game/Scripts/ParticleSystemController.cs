using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private List<GrabMashine> _grabMashines;
    private ParticleSystem _particleSystem;

    private Vector3 _velosityParticle;
    private float _stepSizeDown;
    private float _minSizeParticle;
    public int maxQuantityParticles;
    public int _allQuantityParticles;

    public UnityAction<ParticleSystem.Particle> CatchParticle;
    public UnityAction CatchAllParticle;
    public UnityAction<bool> ContactAirZone;

    private List<Collider> _leavesTanks;


    private int _quantityAllParticles;
    private int percentAll;
    private int percent;
    private int minQuantityAllPsrticles;


    private void Start()
    {
        _quantityAllParticles = GetComponent<ParticleSystem>().maxParticles;
        percentAll = 100;
        percent = 15;
        minQuantityAllPsrticles = (_quantityAllParticles / percentAll) * percent;


        float maxVelocity = 100f;
        _particleSystem = GetComponent<ParticleSystem>();

        _leavesTanks = new List<Collider>();
        maxQuantityParticles = _particleSystem.maxParticles;
        _velosityParticle = new Vector3(maxVelocity, maxVelocity, maxVelocity);
        _stepSizeDown = 0.05f;
        _minSizeParticle = 0.3f;


        for (int i = 0; i < _grabMashines.Count; i++)
        {
            Collider colliderLeavesTank = _grabMashines[i].GetComponentInChildren<LeavesTank>().GetComponent<Collider>();
            _leavesTanks.Add(colliderLeavesTank);
        }

    }

    private void OnParticleCollision(GameObject other)
    {

        _allQuantityParticles = _particleSystem.particleCount;

        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        int numInside = _particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

        for (int i = 0; i < _grabMashines.Count; i++)
        {
            for (int j = 0; j < numInside; j++)
            {
                ParticleSystem.Particle particle = inside[j];
                if (_leavesTanks[i].bounds.Contains(particle.position))
                {
                    if (particle.startSize < _minSizeParticle)
                    {
                        _grabMashines[i].OnGetParticle();
                        particle.velocity = _velosityParticle;
                    }
                    else
                    {
                        particle.startSize -= _stepSizeDown;

                    }
                    inside[j] = particle;
                }
            }
            _particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        }




        if (_allQuantityParticles <= minQuantityAllPsrticles)
        {
            CatchAllParticle?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
