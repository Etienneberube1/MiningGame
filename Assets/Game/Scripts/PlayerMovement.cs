using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [Tooltip("This Values is getting multiply by 10")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _groundDrag;

    private Vector3 _moveDirection;
    private float _horizontalInput;
    private float _verticalInput;


    [Header("References")]
    [SerializeField] private Transform _orientation;
    private Rigidbody _rigidBody;


    [Header("References")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        HandleInput();
        GroundCheck();
        HandleDrag();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleDrag()
    {
        if (_isGrounded) { _rigidBody.linearDamping = _groundDrag; }
        else { _rigidBody.linearDamping = 0; }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private void MovePlayer()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        _rigidBody.AddForce(_moveDirection.normalized * _moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z);

        // Limit le Velocity if needed
        if (flatVelocity.magnitude > _moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _moveSpeed;
            _rigidBody.linearVelocity = new Vector3(limitedVelocity.x, _rigidBody.linearVelocity.y, limitedVelocity.z);
        }
    }
    private void HandleInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }
}
