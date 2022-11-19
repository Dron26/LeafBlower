using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Service
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystemController> _particleSystems;

        public List<ParticleSystemController> GetParticleList()
        {
            List<ParticleSystemController> tempSystems = new();

            for (int i = 0; i < _particleSystems.Count; i++)
            {
                tempSystems.Add(_particleSystems[i]);
            }

            return tempSystems;
        }
    }
}
