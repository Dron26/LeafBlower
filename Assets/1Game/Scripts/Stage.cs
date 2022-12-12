using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Service
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystemController> _particleSystems;
        
        private ExitPanelUI _exitPanel;
            
        public List<ParticleSystemController> GetParticleList()
        {
            List<ParticleSystemController> tempSystems = new();

            for (int i = 0; i < _particleSystems.Count; i++)
            {
                tempSystems.Add(_particleSystems[i]);
            }

            return tempSystems;
        }

        public void SetExitPanel(ExitPanelUI exitPanelUI)
        {
            _exitPanel = exitPanelUI;
        }
        
    }
}
