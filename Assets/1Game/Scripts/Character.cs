using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{
    private CharacterMove _characterMove;
    private float _speed;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMove = GetComponent<CharacterMove>();
    }

    private void Update()
    {
            
    }


}
