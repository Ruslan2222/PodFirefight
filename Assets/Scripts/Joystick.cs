using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Joystick : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _gameCamera;

    [Header("Joystick")]
    [Space]
    [SerializeField] private UltimateJoystick _movement;
    [SerializeField] private float _movementSpeed;
    [Space]
    [SerializeField] private UltimateTouchpad _rotation;
    [SerializeField] private float _rotationSpeed;

    private Transform _transform;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        Vector3 forward = _gameCamera.transform.forward;
        Vector3 right = _gameCamera.transform.right;

        float h = _movement.GetHorizontalAxisRaw();
        float v = _movement.GetVerticalAxisRaw();

        float y = _rotation.GetHorizontalAxis();

        Vector3 movementDirection = forward * v + right * h;

        _rigidbody.velocity = new Vector3(movementDirection.x * _movementSpeed, _rigidbody.velocity.y, movementDirection.z * _movementSpeed) * Time.deltaTime;

        _transform.Rotate(0, y * _rotationSpeed * Time.deltaTime, 0, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            gameObject.transform.position = new Vector3(Random.insideUnitCircle.x * 5, 0);
        }
    }

}
