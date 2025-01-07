using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Raccons_House_Games
{
    public class PlayerControll : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerBody;
        [SerializeField] private FixedJoystick _movementJoystick;
        [SerializeField] private float _baseSpeed = 10.0f;
        [SerializeField] private float _maxSpeed = 20.0f;
        [SerializeField] private float _accelerationTime = 3.0f;

        private float _currentSpeed;
        private float _accelerationTimer;
        private float _checkSpeed; 
        private Vector2 _currentInput;

        public void SetSpeedMultiplier(float multiplier)
        {
            _currentSpeed = _baseSpeed * multiplier;
        }

        private void Start()
        {
            _currentSpeed = _baseSpeed;
            _accelerationTimer = 0.0f;
        }

        private void Update()
        {
            HandleMove();
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

            Vector3 inputDirection = new Vector3(_currentInput.x, 0, _currentInput.y).normalized;
            Vector3 movement = inputDirection * _currentSpeed;

            _playerBody.velocity = new Vector3(movement.x, _playerBody.velocity.y, movement.z);

            _checkSpeed = new Vector3(_playerBody.velocity.x, 0, _playerBody.velocity.z).magnitude;
            
            if (_currentInput != Vector2.zero)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(_playerBody.velocity.x, 0, _playerBody.velocity.z));
            }
        }
    }
}
