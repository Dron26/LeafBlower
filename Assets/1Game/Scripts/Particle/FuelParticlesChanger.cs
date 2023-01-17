using _1Game.Scripts.Empty;
using _1Game.Scripts.Item;
using UnityEngine;

namespace _1Game.Scripts.Particle
{
    public class FuelParticlesChanger : MonoBehaviour
    {
        [SerializeField] private FuelChanger _fuelChanger;

        private RefuelFuel _refuel;
        private ParticleSystem _refuelParticle;
        private ReachedMaxFuelLevel _maxLevel;
        private ParticleSystem _maxLevelParticle;
        
        private void Awake()
        {
            _refuel = GetComponentInChildren<RefuelFuel>();
            _refuelParticle=_refuel.GetComponent<ParticleSystem>();
            _refuelParticle.Stop();
            
            _maxLevel=GetComponentInChildren<ReachedMaxFuelLevel>();
            _maxLevelParticle=_maxLevel.GetComponent<ParticleSystem>();
            _maxLevelParticle.Stop();
        }

        private void OnEnable()
        {
           _fuelChanger.StartRefuel += OnStartRefuel;
           _fuelChanger.StopRefuel += OnStopRefuel;
           _fuelChanger.ReachedMaxLevel += OnReachedMaxLevel;
        }

        private void OnDisable()
        {
            _fuelChanger.StartRefuel -= OnStartRefuel;
            _fuelChanger.StopRefuel -= OnStopRefuel;
            _fuelChanger.ReachedMaxLevel -= OnReachedMaxLevel;
        }

        private void OnStartRefuel()
        {
            ChangeState(true);
        }
        
        private void OnStopRefuel()
        {
            ChangeState(false);
        }

        private void OnReachedMaxLevel()
        {
            OnStopRefuel();
            _maxLevelParticle.Play();
        }
        
        private void ChangeState (bool isRefuel)
        {

            if (isRefuel)
            {
                _refuelParticle.Play();
            }
            else
            {
                _refuelParticle.Stop();
            }
        }
    }
}