using Asteroids.Pooling;
using VContainer.Unity;

namespace Asteroids.Core {
    public class Bootstrapper : IStartable {
        private readonly PoolRegistry _poolRegistry;
        private readonly Wave.WaveSystem _waveSystem;

        public Bootstrapper(PoolRegistry poolRegistry, Wave.WaveSystem waveSystem) {
            _poolRegistry = poolRegistry;
            _waveSystem = waveSystem;
        }

        public void Start() {
            _poolRegistry.OnReady += OnPoolsReady;
        }

        private void OnPoolsReady() {
            _poolRegistry.OnReady -= OnPoolsReady;
            _waveSystem.StartWave();
        }
    }
}