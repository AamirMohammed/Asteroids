using Asteroids.Pooling;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;
using UnityEngine;

namespace Asteroids.Core {
    public class BootState : IState {
        private readonly PoolRegistry _poolRegistry;
        private readonly ShipMovement _shipMovement;
        private readonly WaveSystem _waveSystem;

        public BootState(PoolRegistry poolRegistry, ShipMovement shipMovement, WaveSystem waveSystem) {
            _poolRegistry = poolRegistry;
            _shipMovement = shipMovement;
            _waveSystem = waveSystem;
        }

        public void OnEnter() {
            _poolRegistry.Initialize();
        }

        public void OnExit() {
            StartGame();
        }

        public void Tick() {
        }

        private void StartGame() {
            _shipMovement.gameObject.SetActive(true);
            _shipMovement.Teleport(Vector3.zero);
            _waveSystem.StartWave();
        }
    }
}