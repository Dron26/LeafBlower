using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{

    private CharacterMove _characterMove;
    private float _speed;
    private Animator _animator;
    private int SpeedHashName;



    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMove = GetComponent<CharacterMove>();
        SpeedHashName = Animator.StringToHash("Speed");

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        _speed = _characterMove.Speed;
        _animator.SetFloat(SpeedHashName, _speed);
    }


}
