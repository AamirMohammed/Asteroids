using Asteroids.HealthSystem;
using Asteroids.Ship;
using FSM;
using UnityEngine;

namespace Asteroids.Core {
    public class PlayingState : IState {
        private readonly IHealth _health;
        private readonly IShip _ship;

        private bool _isRespawning;
        private float _respawnTimer;
        private const float RespawnDelay = 2f;

        public PlayingState(IHealth health, IShip ship) {
            _health = health;
            _ship = ship;
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
                _ship.Show();
                _ship.Teleport(Vector3.zero);
            }
        }

        private void OnLivesChanged(int lives) {
            _ship.Hide();
            _isRespawning = true;
            _respawnTimer = RespawnDelay;
        }
    }
}