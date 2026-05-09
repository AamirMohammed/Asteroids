using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Input {
    public class InputReader : MonoBehaviour, IInputReader,
        AsteroidsInputActions.IPlayerActions {
        public event Action ShootPressed;
        public event Action<float> RotateInput;
        public event Action<bool> ThrustInput;

        private AsteroidsInputActions _inputActions;

        private void Awake() {
            _inputActions = new AsteroidsInputActions();
            _inputActions.Player.SetCallbacks(this);
        }

        private void OnEnable() {
            _inputActions.Player.Enable();
        }

        private void OnDisable() {
            _inputActions.Player.Disable();
        }

        // Invoked on all phases so value resets to false on release
        public void OnThrust(InputAction.CallbackContext context) {
            ThrustInput?.Invoke(context.ReadValueAsButton());
        }

        // Invoked on all phases so value resets to 0 on release
        public void OnRotate(InputAction.CallbackContext context) {
            RotateInput?.Invoke(context.ReadValue<float>());
        }

        public void OnShoot(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Performed) {
                ShootPressed?.Invoke();
            }
        }
    }
}