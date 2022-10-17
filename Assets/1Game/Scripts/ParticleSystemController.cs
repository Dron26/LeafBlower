using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private List<GrabMashine> _grabMashine;
    private ParticleSystem _particleSystem;

    private Vector3 _velosityParticle;
    private float _stepSizeDown;
    private float _minSizeParticle;

    public UnityAction<ParticleSystem.Particle> CatchParticle;


    private void Start()
    {
        float maxVelocity = 100f;
        _velosityParticle = new Vector3(maxVelocity, maxVelocity, maxVelocity);
        _stepSizeDown = 0.1f;
        _minSizeParticle = 0.3f;
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<GrabMashine>(out GrabMashine grabMashine))
        {

            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            int numInside = _particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

            for (int i = 0; i < _grabMashine.Count; i++)
            {
                for (int j = 0; j < numInside; j++)
                {
                    ParticleSystem.Particle particle = inside[j];

                    if (particle.startSize < _minSizeParticle)
                    {
                        _grabMashine[i].OnGetParticle();
                        particle.velocity = _velosityParticle;
                    }
                    else
                    {
                        particle.startSize += _stepSizeDown;

                    }
                    inside[j] = particle;


                    _particleSystem.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
                }
            }
        }
    }
}
//CatchParticle?.Invoke(particle);