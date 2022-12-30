using System.Collections.Generic;
using _1Game.Scripts.Empty;
using _1Game.Scripts.WorkPlaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Particle
{
    public class ParticleSystemController : MonoBehaviour
    {
        private List<GrabMashine> _grabMashines = new List<GrabMashine>();
        private WorkPlace _workPlace;
        private ParticleSystem _particleSystem;
        private Vector3 _velosityParticle;
        private List<Collider> _leavesTanks;
        
        private float _stepSizeDown;
        private float _minSizeParticle;
        public int MaxQuantityParticles;
        public int AllQuantityParticles;
        private int _quantityAllParticles;
        private int percentAll;
        private int percent;
        private int minQuantityAllPsrticles;
       
        public UnityAction<ParticleSystem.Particle> CatchParticle;
        public UnityAction CatchAllParticle;
        public UnityAction<bool> ContactAirZone;


        private void Awake()
        {
            _workPlace = GetComponentInParent<WorkPlace>();
            _particleSystem = GetComponent<ParticleSystem>();
            _quantityAllParticles = GetComponent<ParticleSystem>().main.maxParticles;
        }

        private void Start()
        {
            percentAll = 100;
            percent = 15;
            minQuantityAllPsrticles = (_quantityAllParticles / percentAll) * percent;
            float maxVelocity = 100f;
            _leavesTanks = new List<Collider>();
            MaxQuantityParticles = _particleSystem.main.maxParticles;
            _velosityParticle = new Vector3(maxVelocity, maxVelocity, maxVelocity);
            _stepSizeDown = 0.08f;
            _minSizeParticle = 0.3f;
            GetMashines();
            GetLeavesTanks();
        }

        private void Update()
        {
            AllQuantityParticles = _particleSystem.particleCount;

            if (AllQuantityParticles <= minQuantityAllPsrticles)
            {
                CatchAllParticle?.Invoke();
                _particleSystem.gameObject.SetActive(false);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
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
        }

        private void GetMashines()
        {
            foreach (GrabMashine mashine in _workPlace.GetComponentsInChildren<GrabMashine>())
            {
                _grabMashines.Add(mashine);
            }
        }

        private void GetLeavesTanks()
        {
            for (int i = 0; i < _grabMashines.Count; i++)
            {
                Collider colliderLeavesTank =
                    _grabMashines[i].GetComponentInChildren<LeavesTank>().GetComponent<Collider>();
                _leavesTanks.Add(colliderLeavesTank);
            }
        }
    }
}