using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Raccons_House_Games
{
    public class PlayerControll : Player
    {
        [SerializeField] private Rigidbody _playerBody;
        [SerializeField] private FixedJoystick _movementJoystick;
        private Player _player;
        private Vector2 _currentInput;
        private float _checkSpeed;

        private void Update()
        {
            HandleMove();
        }

        private void HandleMove()
        {
            _currentInput = new Vector2(_movementJoystick.Horizontal, _movementJoystick.Vertical);
            Vector3 movement = new Vector3(_currentInput.x * 10.0f, _playerBody.velocity.y, _currentInput.y * 10.0f);

            _playerBody.velocity = movement;
            _checkSpeed = new Vector3(_playerBody.velocity.x, 0, _playerBody.velocity.z).magnitude;

            if(_currentInput != Vector2.zero)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(_playerBody.velocity.x, 0, _playerBody.velocity.z));
            }
        }
    }
}
