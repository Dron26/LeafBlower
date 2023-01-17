using System;
using System.Collections;
using _1Game.Scripts.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Particle
{
    public class DirectionArrow : MonoBehaviour
    {
        private CharacterTutorial _characterTutorial;
        private ParticleSystem _arrows;
        
        private Vector3 _point;
        private bool isLook;
        private bool _isTutorialCopletaded;

        private void Awake()
        {
            _arrows = GetComponent<ParticleSystem>();
            _characterTutorial=GetComponentInParent<CharacterTutorial>();
        }

        private void OnEnable()
        {
            _characterTutorial.ReachedCart += SetBoolIsLook;
            _characterTutorial.ReachedPoint += SetBoolIsLook;
        }

        private void OnDisable()
        {
            _characterTutorial.ReachedCart -= SetBoolIsLook;
            _characterTutorial.ReachedPoint -= SetBoolIsLook;
        }

        private void  SetLookAt()
        {

                if (isLook)
                {
                    _arrows.gameObject.SetActive(true);
                    transform.LookAt(_point);
                }
                else
                {
                    _arrows.gameObject.SetActive(false);
                }

        }

        public void SetLookPoint(Vector3 point)
        {
            _point = _point;
            StartTutorial();
        }
        
        public void SetBoolIsLook()
        {
            isLook = !isLook;
        }

        public void EndTutorial()
        { 
            isLook = false;
            SetLookAt();
        }
        
        public void StartTutorial()
        {
            isLook = true;
            SetLookAt();
        }
        
        
    }
}
