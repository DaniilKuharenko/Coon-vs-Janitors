using UnityEngine;

namespace Raccons_House_Games
{
    public class PlayerControll : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerBody;
        [SerializeField] private FixedJoystick _movementJoystick;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _baseSpeed = 10.0f;
        [SerializeField] private float _maxSpeed = 10.5f;
        [SerializeField] private float _accelerationTime = 3.0f;

        private float _currentDuration = 0.0f;
        private float _currentSpeed;
        private float _accelerationTimer;
        private float _checkSpeed;
        private Vector2 _currentInput;

        public void SetSpeedMultiplier(float multiplier, float duration)
        {
            _maxSpeed += multiplier;
            _currentDuration = duration;
            _accelerationTimer = 0.0f;
            Debug.LogError($"SetSpeedMultiplier called with multiplier: {multiplier}, _currentSpeed: {_maxSpeed}");
        }

        public void ResetSpeed()
        {
            _maxSpeed = 10.5f;
            _accelerationTimer = 0.0f;
        }

        private void Start()
        {
            _currentSpeed = _baseSpeed;
            _accelerationTimer = 0.0f;
        }

        private void Update()
        {
            HandleSpeedReset();
        }

        private void FixedUpdate()
        {
            HandleMove();
        }

        private void HandleSpeedReset()
        {
            if (_currentDuration > 0)
            {
                _currentDuration -= Time.deltaTime;
                if (_currentDuration <= 0)
                {
                    ResetSpeed();
                }
            }
        }

        private void HandleMove()
        {
            _currentInput = new Vector2(_movementJoystick.Horizontal, _movementJoystick.Vertical);

            if (_currentInput != Vector2.zero)
            {
                _accelerationTimer += Time.deltaTime;
                _currentSpeed = Mathf.Lerp(_baseSpeed, _maxSpeed, _accelerationTimer / _accelerationTime);
            }
            else
            {
                _accelerationTimer = 0.0f;
                _currentSpeed = _baseSpeed;
            }

            // camera rotation angle along the Y axis
            float cameraAngle = _camera.transform.eulerAngles.y;

            Vector3 inputDirection = new Vector3(_currentInput.x, 0, _currentInput.y).normalized;
            Quaternion rotation = Quaternion.Euler(0, cameraAngle, 0);
            Vector3 movementDirection = rotation * inputDirection;

            Vector3 movement = movementDirection * _currentSpeed * Time.fixedDeltaTime;

            if (_currentInput != Vector2.zero)
            {
                Vector3 targetPosition = _playerBody.position + movement;
                _playerBody.MovePosition(targetPosition);

                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movementDirection.x, 0, movementDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);
            }
            
            _checkSpeed = new Vector3(_playerBody.velocity.x, 0, _playerBody.velocity.z).magnitude;
        }
    }
}
