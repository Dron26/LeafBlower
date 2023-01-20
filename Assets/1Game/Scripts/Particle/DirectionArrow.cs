using System;
using System.Collections;
using _1Game.Scripts.Core;
using _1Game.Scripts.Empty;
using _1Game.Scripts.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Particle
{
    public class DirectionArrow : MonoBehaviour
    {
        [SerializeField] private ExitPanel _exit;
        
        private CharacterTutorial _characterTutorial;
        private ParticleSystem _arrows;
        private DirectionArrowsParticle _directionArrowComponent;
        private Vector3 _point;
        
        private bool isLook;
        private bool _isTutorialCopletaded;

        private void Awake()
        {
            _arrows = GetComponent<ParticleSystem>();
            _characterTutorial = GetComponentInParent<CharacterTutorial>();
            _directionArrowComponent=GetComponentInChildren<DirectionArrowsParticle>();
        }

        private void OnEnable()
        {
            _characterTutorial.ReachedCart += ReachedPoint;
            _characterTutorial.ReachedPoint += ReachedPoint;
            _characterTutorial.ReachedRefuel += ReachedPoint;
            _exit.SetNextLevel += ReachedExit;
        }

        private void OnDisable()
        {
            _characterTutorial.ReachedCart -= ReachedPoint;
            _characterTutorial.ReachedPoint -= ReachedPoint;
            _characterTutorial.ReachedRefuel -= ReachedPoint;
            _exit.SetNextLevel += ReachedExit;
        }

        private IEnumerator SetLookAt()
        {
            while (_isTutorialCopletaded==false)
            {
                while (isLook)
                {
                        transform.LookAt(_point);
                        yield return null;
                }
                yield return null;
            }
            
            yield break;
        }

        public void SetLookPoint(Vector3 point)
        {
            _point = point;
            isLook = true;
            _directionArrowComponent.gameObject.SetActive(true);
        }

        public void ReachedPoint()
        {
            isLook = false;
            _directionArrowComponent.gameObject.SetActive(false);
        }
        
        public void ReachedExit()
        {
            _isTutorialCopletaded = true;
        }

        public void EndTutorial()
        {
            isLook = false;
            StopCoroutine(SetLookAt());
        }

        public void StartTutorial()
        {
            isLook = true;
            StartCoroutine(SetLookAt());
        }
    }
}