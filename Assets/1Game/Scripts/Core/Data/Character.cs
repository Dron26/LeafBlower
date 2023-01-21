using UnityEngine;

namespace _1Game.Scripts.Core
{
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        private CharacterMove _characterMove;
        private float _speed;
        private Animator _animator;
        private readonly int _speedHashName = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterMove = GetComponent<CharacterMove>();
        }

        private void Update()
        {
            _speed = _characterMove.Speed;
            _animator.SetFloat(_speedHashName, _speed);
        }
    }
}