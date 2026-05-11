using Asteroids.Pooling;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;
using UnityEngine;

namespace Asteroids.Core {
    public class BootState : IState {
        private readonly PoolRegistry _poolRegistry;
        private readonly IShip _ship;
        private readonly WaveSystem _waveSystem;

        public BootState(PoolRegistry poolRegistry, IShip ship, WaveSystem waveSystem) {
            _poolRegistry = poolRegistry;
            _ship = ship;
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
            _ship.Show();
            _ship.Teleport(Vector3.zero);
            _waveSystem.StartWave();
        }
    }
}