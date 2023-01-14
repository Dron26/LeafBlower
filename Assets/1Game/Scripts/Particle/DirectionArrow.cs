using System;
using System.Collections;
using UnityEngine;

namespace _1Game.Scripts.Particle
{
    public class DirectionArrow : MonoBehaviour
    { 
        [SerializeField] public WorkplaceTutorialPoint _workplaceTutorialPoint;

        private Vector3 _point;
        private bool isLook;

        private void Awake()
        {
            _point=_workplaceTutorialPoint.transform.position;
        }
        
        private IEnumerator LookAt()
        {
            while (isLook)
            {
                transform.LookAt(_point);

                yield return null;
            }
        }

        public void SetLookPoint(Vector3 point)
        {
            
        }
        
        public void SetBoolIsLook()
        {
            isLook = !isLook;
        }

        public void EndTutorial()
        {
            StopCoroutine(LookAt());
        }
        
        public void StartTutorial()
        {
            StartCoroutine(LookAt());
        }
    }
}
