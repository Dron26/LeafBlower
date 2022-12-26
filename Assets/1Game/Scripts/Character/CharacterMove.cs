using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _dynamicJoystick;

    public float Speed => _speed;
    
    
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private float _speed;
    private float _maxSpeed;
    private float _horizontal => Input.GetAxis(Horizontal) ;
    private float _vertical => Input.GetAxis(Vertical);
    private void Start()
    {
        _maxSpeed = 1f;
    }

    private void Update()
    {
        float magnitude = _dynamicJoystick.Magnitude;


        if (_dynamicJoystick.Horizontal != 0 || _dynamicJoystick.Vertical != 0)
        {
            Vector3 moveVector = (Vector3.right * _dynamicJoystick.Horizontal + Vector3.forward * _dynamicJoystick.Vertical);
            Move(moveVector);
        }
        else if ((Input.GetAxis(Horizontal) != 0) || (Input.GetAxis(Vertical) != 0))
        {
            Vector3 moveVector = (Vector3.right *  +_horizontal+ Vector3.forward * _vertical);
            Move(moveVector);
        }
        else
        {
            magnitude = 0;
        }

        _speed = Mathf.Clamp(magnitude, 0, _maxSpeed) * 2;
    }

    public void Move(Vector3 moveVector)
    {
        moveVector.Normalize();
        LookAt(moveVector);
        transform.Translate(moveVector * _speed * Time.deltaTime, Space.World);
    }

    public void LookAt(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }
}