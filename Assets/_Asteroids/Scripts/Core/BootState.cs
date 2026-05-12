using Asteroids.Pooling;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;

namespace Asteroids.Core {
    public class BootState : IState {
        private readonly IPoolRegistry _poolRegistry;
        private readonly IShipSpawnService _shipSpawnService;
        private readonly IWaveService _waveService;

        public BootState(IPoolRegistry poolRegistry, IShipSpawnService shipSpawnService, IWaveService waveService) {
            _poolRegistry = poolRegistry;
            _shipSpawnService = shipSpawnService;
            _waveService = waveService;
        }

        public void OnEnter() {
            _poolRegistry.Initialize();
        }

        public void OnExit() {
            _shipSpawnService.Spawn();
            _waveService.StartWave();
        }

        public void Tick() {
        }
    }
}