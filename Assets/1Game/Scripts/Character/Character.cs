using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{

    private CharacterMove _characterMove;
    private float _speed;
    private Animator _animator;
    private int SpeedHashName;
    private GameObject _currentStage;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMove = GetComponent<CharacterMove>();
        SpeedHashName = Animator.StringToHash("Speed");

    }


    private void Update()
    {
        _speed = _characterMove.Speed;
        _animator.SetFloat(SpeedHashName, _speed);
    }


    public void SetStage(GameObject currentStage)
    {
        _currentStage = currentStage;
    }
}
