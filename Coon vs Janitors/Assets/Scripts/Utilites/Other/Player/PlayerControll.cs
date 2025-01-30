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

        public float CheckSpeed => _checkSpeed;

        private Vector3 _previousPosition;
        private float _previousTime;


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

        public void StartPlayer()
        {
            _currentSpeed = _baseSpeed;
            _accelerationTimer = 0.0f;
            _previousPosition = _playerBody.position;
            _previousTime = Time.time;
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
                // Acceleration when the joystick is active
                _accelerationTimer += Time.deltaTime;
                _currentSpeed = Mathf.Lerp(_baseSpeed, _maxSpeed, _accelerationTimer / _accelerationTime);

                // Calculate movement direction
                float cameraAngle = _camera.transform.eulerAngles.y;
                Vector3 inputDirection = new Vector3(_currentInput.x, 0, _currentInput.y).normalized;
                Quaternion rotation = Quaternion.Euler(0, cameraAngle, 0);
                Vector3 movementDirection = rotation * inputDirection;

                Vector3 movement = movementDirection * _currentSpeed * Time.fixedDeltaTime;

                // Move and rotate the player
                Vector3 targetPosition = _playerBody.position + movement;
                _playerBody.MovePosition(targetPosition);

                Quaternion targetRotation = Quaternion.LookRotation(new Vector3(-movementDirection.x, 0, -movementDirection.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10);

                // Update speed calculation
                float currentTime = Time.time;
                float deltaTime = currentTime - _previousTime;

                if (deltaTime > 0)
                {
                    Vector3 deltaPosition = _playerBody.position - _previousPosition;
                    _checkSpeed = deltaPosition.magnitude / deltaTime;
                }

                // Save state for the next frame
                _previousPosition = _playerBody.position;
                _previousTime = currentTime;
            }
            else
            {
                // Reset speed if the joystick is not in use
                _accelerationTimer = 0.0f;
                _currentSpeed = _baseSpeed;
                _checkSpeed = 0.0f; // Speed is set to zero when there is no movement
            }
        }

    }
}
