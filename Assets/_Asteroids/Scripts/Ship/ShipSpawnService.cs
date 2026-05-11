using Asteroids.Core;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Ship {
    public class ShipSpawnService : IShipSpawnService, ITickable {
        private readonly IShip _ship;
        private readonly IPlayerConfig _playerConfig;

        private bool _isRespawning;
        private float _respawnTimer;

        public ShipSpawnService(IShip ship, IPlayerConfig playerConfig) {
            _ship = ship;
            _playerConfig = playerConfig;
        }

        public void Spawn() {
            _isRespawning = false;
            _ship.Teleport(Vector3.zero);
            _ship.Show();
        }

        public void ScheduleRespawn() {
            _ship.Hide();
            _isRespawning = true;
            _respawnTimer = _playerConfig.RespawnDelay;
        }

        public void Tick() {
            if (!_isRespawning) {
                return;
            }

            _respawnTimer -= Time.deltaTime;
            if (_respawnTimer <= 0f) {
                Spawn();
            }
        }
    }
}