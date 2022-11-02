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
        _maxSpeed = 1.3f;
       
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float magnitude = _dynamicJoystick.Magnitude;

        

        if (_dynamicJoystick.Horizontal != 0 || _dynamicJoystick.Vertical != 0)
            {
                Vector3 moveVector = (Vector3.right * _dynamicJoystick.Horizontal + Vector3.forward * _dynamicJoystick.Vertical);
                moveVector.Normalize();
            
            Move(moveVector);
            }
        else
        {
            magnitude = 0;
        }

        _speed = Mathf.Clamp(magnitude, 0, _maxSpeed)*2;
    }


    public void Move(Vector3 moveVector)
    {

        LookAt(moveVector);
        transform.Translate(moveVector * _speed * Time.deltaTime, Space.World);

    }


    public void LookAt(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }


}
