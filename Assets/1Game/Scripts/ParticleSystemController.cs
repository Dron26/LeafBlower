using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField] private List<GrabMashine> _grabMashines;
    private ParticleSystem _particleSystem;

    private Vector3 _velosityParticle;
    private float _stepSizeDown;
    private float _minSizeParticle;

    public UnityAction<ParticleSystem.Particle> CatchParticle;
    private List <Collider> _leavesTanks;


    private void Start()
    {
        float maxVelocity = 100f;
        
        _velosityParticle = new Vector3(maxVelocity, maxVelocity, maxVelocity);
        _stepSizeDown = 0.1f;
        _minSizeParticle = 0.3f;
        _particleSystem = GetComponent<ParticleSystem>();
        _leavesTanks = new List<Collider>();

        for (int i = 0; i < _grabMashines.Count; i++)
        {

            Collider colliderLeavesTank = _grabMashines[i].GetComponentInChildren<LeavesTank>().GetComponent<Collider>();
            _leavesTanks.Add(colliderLeavesTank);


        }
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<LeavesTank>(out LeavesTank leavesTank))
        {

            List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
            int numInside = _particleSystem.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

            for (int i = 0; i < _grabMashines.Count; i++)
            {
                for (int j = 0; j < numInside; j++)
                {
                    ParticleSystem.Particle particle = inside[j];
                    if (_leavesTanks)
)
                    {

                    }

                    if (particle.startSize < _minSizeParticle)
                    {
                        _grabMashines[i].OnGetParticle();
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