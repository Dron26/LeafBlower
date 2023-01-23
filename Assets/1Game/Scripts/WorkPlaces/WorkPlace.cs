using _1Game.Scripts.Particle;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.WorkPlaces
{
    public class WorkPlace : MonoBehaviour
    {
        public UnityAction<bool> EnterWorkPlace;
        public bool IsCleaned => _isCleaned;
        private bool _isCleaned;
        
        private ParticleSystemController _particleSystem;
        
        private void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystemController>();
        }
        
        private void OnEnable()
        {
            _particleSystem.CatchAllParticle += OnCatchAllParticle;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                EnterWorkPlace?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Core.Character character))
            {
                EnterWorkPlace?.Invoke(false);
            }
        }

        private void OnCatchAllParticle()
        {
            _isCleaned = true;
        }
    }
}