using Asteroids.Pooling;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;

namespace Asteroids.Core {
    public class BootState : IState {
        private readonly IPoolRegistry _poolRegistry;
        private readonly IShipSpawnService _shipSpawnService;
        private readonly IWaveSystem _waveSystem;

        public BootState(IPoolRegistry poolRegistry, IShipSpawnService shipSpawnService, IWaveSystem waveSystem) {
            _poolRegistry = poolRegistry;
            _shipSpawnService = shipSpawnService;
            _waveSystem = waveSystem;
        }

        public void OnEnter() {
            _poolRegistry.Initialize();
        }

        public void OnExit() {
            _shipSpawnService.Spawn();
            _waveSystem.StartWave();
        }

        public void Tick() {
        }
    }
}