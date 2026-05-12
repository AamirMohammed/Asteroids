using System;
using Asteroids.Asteroid;
using Asteroids.EventChannels;
using VContainer.Unity;

namespace Asteroids.Wave {
    public class WaveSystem : IWaveSystem, IInitializable, IDisposable {
        private readonly IAsteroidSpawnService _spawnService;
        private readonly IReadOnlyEventChannel<AsteroidDestroyedData> _destroyedChannel;
        private readonly IAsteroidConfig _largeAsteroidConfig;
        private readonly IWaveConfig _waveConfig;

        private int _asteroidsRemaining;
        private int _currentWave;

        public WaveSystem(IAsteroidSpawnService spawnService,
            IReadOnlyEventChannel<AsteroidDestroyedData> destroyedChannel,
            IAsteroidConfig largeAsteroidConfig, IWaveConfig waveConfig) {
            _spawnService = spawnService;
            _destroyedChannel = destroyedChannel;
            _largeAsteroidConfig = largeAsteroidConfig;
            _waveConfig = waveConfig;
        }

        public void Initialize() {
            _destroyedChannel.Published += OnAsteroidDestroyed;
        }

        public void Dispose() {
            _destroyedChannel.Published -= OnAsteroidDestroyed;
        }

        public void StartWave() {
            _currentWave++;
            int count = _waveConfig.GetAsteroidCount(_currentWave);
            _asteroidsRemaining = CalculateTotalAsteroids(_largeAsteroidConfig, count);
            _spawnService.SpawnWave(count, _largeAsteroidConfig);
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedData data) {
            _asteroidsRemaining--;
            if (_asteroidsRemaining <= 0) {
                StartWave();
            }
        }

        private int CalculateTotalAsteroids(IAsteroidConfig config, int count) {
            int total = 0;
            int currentCount = count;
            IAsteroidConfig currentConfig = config;

            while (currentConfig != null) {
                total += currentCount;
                currentCount *= currentConfig.SplitCount;
                currentConfig = currentConfig.SplitIntoConfig;
            }

            return total;
        }

        public void Reset() {
            _currentWave = 0;
        }
    }
}