using Asteroids.HealthSystem;
using Asteroids.Ship;
using FSM;
using UnityEngine;

namespace Asteroids.Core {
    public class PlayingState : IState {
        private readonly IHealth _health;
        private readonly ShipMovement _shipMovement;

        private bool _isRespawning;
        private float _respawnTimer;
        private const float RespawnDelay = 2f;

        public PlayingState(IHealth health, ShipMovement shipMovement) {
            _health = health;
            _shipMovement = shipMovement;
        }

        public void OnEnter() {
            _isRespawning = false;
            _health.LivesChanged += OnLivesChanged;
        }

        public void OnExit() {
            _health.LivesChanged -= OnLivesChanged;
        }

        public void Tick() {
            if (!_isRespawning) {
                return;
            }

            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0f) {
                _isRespawning = false;
                _shipMovement.gameObject.SetActive(true);
                _shipMovement.Teleport(Vector3.zero);
            }
        }

        private void OnLivesChanged(int lives) {
            _shipMovement.gameObject.SetActive(false);
            _isRespawning = true;
            _respawnTimer = RespawnDelay;
        }
    }
}