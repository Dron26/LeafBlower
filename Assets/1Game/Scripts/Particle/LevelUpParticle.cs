using System;
using _1Game.Scripts.Core;
using UnityEngine;

namespace _1Game.Scripts.Particle
{
    public class LevelUpParticle : MonoBehaviour
    {
        [SerializeField] private UpgradeParametrs _parametrs;
        private ParticleSystem _particleSystem;

        private void Awake()
        {
            _particleSystem=GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            _parametrs.UpCart += PlayParticle;
            _parametrs.UpFuel += PlayParticle;
            _parametrs.UpPower += PlayParticle;
        }

        private void PlayParticle(int value)
        {
            _particleSystem.Play();
        }
        
        private void OnDisable()
        {
            _parametrs.UpCart -= PlayParticle;
            _parametrs.UpFuel -= PlayParticle;
            _parametrs.UpPower -= PlayParticle;
        }
    }
}
