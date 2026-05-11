using Asteroids.Input;
using UnityEngine;
using VContainer;

namespace Asteroids.Ship {
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipMovement : MonoBehaviour {
        private Rigidbody2D _rigidbody2D;
        private IInputReader _inputReader;
        private IShipConfig _shipConfig;
        private bool _isThrusting;
        private float _rotationInput;

        [Inject]
        public void Construct(IInputReader inputReader, IShipConfig shipConfig) {
            _inputReader = inputReader;
            _shipConfig = shipConfig;
        }

        private void Awake() {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable() {
            _inputReader.ThrustInput += OnThrustInput;
            _inputReader.RotateInput += OnRotateInput;
        }

        private void OnDisable() {
            _inputReader.ThrustInput -= OnThrustInput;
            _inputReader.RotateInput -= OnRotateInput;
        }

        private void FixedUpdate() {
            if (_isThrusting) {
                _rigidbody2D.AddForce(transform.up * _shipConfig.ThrustForce);
            }

            if (_rotationInput != 0) {
                _rigidbody2D.MoveRotation(_rigidbody2D.rotation -
                                          _rotationInput * _shipConfig.RotationSpeed * Time.fixedDeltaTime);
            }
        }

        public void Teleport(Vector3 position) {
            ResetVelocity();
            transform.SetPositionAndRotation(position, Quaternion.identity);
            _rigidbody2D.position = position;
            _rigidbody2D.rotation = 0f;
        }

        private void ResetVelocity() {
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
            _isThrusting = false;
            _rotationInput = 0f;
        }

        private void OnThrustInput(bool isThrusting) {
            _isThrusting = isThrusting;
        }

        private void OnRotateInput(float rotation) {
            _rotationInput = rotation;
        }
    }
}