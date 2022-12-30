using System.Collections.Generic;
using _1Game.Scripts.Particle;
using _1Game.Scripts.UI;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystemController> _particleSystems;

        private ExitPanel _exitPanel;

        public List<ParticleSystemController> GetParticleList()
        {
            List<ParticleSystemController> tempSystems = new();

            for (int i = 0; i < _particleSystems.Count; i++)
            {
                tempSystems.Add(_particleSystems[i]);
            }

            return tempSystems;
        }

        public void SetExitPanel(ExitPanel exitPanel)
        {
            _exitPanel = exitPanel;
        }
    }
}