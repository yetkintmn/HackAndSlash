using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public FloatingJoystick FloatingJoystick;

    [SerializeField] private float moveSpeed;

    private float _heading;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        var direction = Vector3.right * FloatingJoystick.Horizontal +
                     Vector3.forward * FloatingJoystick.Vertical;

        if (!Input.GetMouseButton(0))
            direction = direction.normalized / 1000;

        if (Mathf.Abs(FloatingJoystick.Vertical) > 0.001f ||
            Mathf.Abs(FloatingJoystick.Horizontal) > 0.001f)
        {
            _heading = Mathf.Atan2(direction.x, direction.z);
            transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            _rigidbody.velocity = direction * moveSpeed;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, _heading * Mathf.Rad2Deg, 0);
            _rigidbody.velocity = Vector3.down;
        }
    }
}
