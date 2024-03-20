using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSmoothVelocity;

    private float _minMagnitudeDirection = 0.1f;
    private float _turnSmoothTime;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Vector2 inputDirection = GetInputDirection();

        if (inputDirection.magnitude >= _minMagnitudeDirection)
        {
            float targetAngleY = Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angleY = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngleY, ref _turnSmoothTime, _turnSmoothVelocity);
            transform.rotation = Quaternion.Euler(0, angleY, 0);
            Vector3 moveDirection = (Quaternion.Euler(0f, targetAngleY, 0f) * Vector3.forward).normalized;
            _characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);
        }
    }

    private Vector2 GetInputDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        return new Vector2(horizontalInput, verticalInput).normalized;
    }
}
