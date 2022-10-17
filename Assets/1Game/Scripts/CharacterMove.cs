using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]private DynamicJoystick _dynamicJoystick;

    private bool _isMoving;
    private bool _isCollidingWithObstacle;
    private Vector3 _collisionVector;
    private Animator _animator;
    [SerializeField] private float speed = 0.4f;
    private int IsWalkHashName;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        IsWalkHashName = Animator.StringToHash("IsWalk");
    }

    private void Update()
    {
            if (_dynamicJoystick.Horizontal != 0 || _dynamicJoystick.Vertical != 0)
            {
                Vector3 moveVector = (Vector3.right * _dynamicJoystick.Horizontal + Vector3.forward * _dynamicJoystick.Vertical);
                moveVector.Normalize();
                Move(moveVector);
            }
            else
                StopMove();
    }


    public void Move(Vector3 moveVector)
    {

        LookAt(moveVector);
        transform.Translate(moveVector * speed * Time.deltaTime, Space.World);

        SetState();
        _isMoving = true;
    }

    public void StopMove()
    {
        if (_isMoving)
        {
            _isMoving = false;
            SetState();
        }
    }

    public void LookAt(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetState()
    {
        _animator.SetBool(IsWalkHashName, _isMoving);
    }
}
