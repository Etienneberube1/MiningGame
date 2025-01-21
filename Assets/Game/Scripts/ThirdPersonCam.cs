using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _playerVisual;
    [SerializeField] private Rigidbody _rigidbody;


    [Header("Values")]
    [SerializeField] private float _rotationSpeed;

    private float _horizontalInput;
    private float _verticalInput;

    private void Start()
    {
        HandleCursorState();
    }
    private void Update()
    {
        CalculateOrientation();
        HandleInput();
        HandleRotation();
    }

    private void CalculateOrientation()
    {
        Vector3 viewDirection = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDirection.normalized;
    }

    private void HandleRotation()
    {
        Vector3 inputDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        if (inputDirection != Vector3.zero)
        {
            _playerVisual.forward = Vector3.Slerp(_playerVisual.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
    }

    private void HandleInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void HandleCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

