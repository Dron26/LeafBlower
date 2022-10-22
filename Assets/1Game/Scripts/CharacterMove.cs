using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField]private DynamicJoystick _dynamicJoystick;
    public float Speed { get => _speed; set { } }


    private bool _isMoving;
    private bool _isCollidingWithObstacle;
    private Vector3 _collisionVector;
    private Animator _animator;
    private float _speed ;
    private float _maxSpeed ;

    private int IsWalkHashName;

    private void Start()
    {
        _maxSpeed = 1;
        _speed = Mathf.Clamp( _dynamicJoystick.Magnitude,0, _maxSpeed);
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
        transform.Translate(moveVector * _speed * Time.deltaTime, Space.World);

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
