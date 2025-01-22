using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [Tooltip("This Value is multiplied by 10")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _groundDrag;

    private Vector3 _moveDirection;
    private float _horizontalInput;
    private float _verticalInput;



    [Header("References")]
    [SerializeField] private Transform _orientation;
    private Rigidbody _rigidBody;

    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private bool _isGrounded;

    
    [Header("Animation")]
    [SerializeField] private Animator _animator;

    [Range(0, 1f)]
    public float _startAnimTime = 0.3f;




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
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleDrag()
    {
        _rigidBody.linearDamping = _isGrounded ? _groundDrag : 0f;
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

        // Limit the velocity if needed
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

    private void UpdateAnimation()
    {
        // Calculate speed from the Rigidbody's velocity
        float speed = new Vector3(_rigidBody.linearVelocity.x, 0f, _rigidBody.linearVelocity.z).magnitude;

        // Set the Blend parameter for the animation
        _animator.SetFloat("Blend", speed / _moveSpeed, _startAnimTime, Time.deltaTime);
    }
}
