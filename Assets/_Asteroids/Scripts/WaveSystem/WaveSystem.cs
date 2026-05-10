using System;
using Asteroids.Asteroid;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.WaveSystem {
    public class WaveSystem : IInitializable, IDisposable {
        private readonly AsteroidSpawnService _spawnService;
        private readonly AsteroidDestroyedChannel _destroyedChannel;
        private readonly AsteroidConfig _largeAsteroidConfig;
        private readonly WaveConfig _waveConfig;

        private int _asteroidsRemaining;
        private int _currentWave;

        public WaveSystem(AsteroidSpawnService spawnService, AsteroidDestroyedChannel destroyedChannel,
            AsteroidConfig largeAsteroidConfig, WaveConfig waveConfig) {
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
            Debug.Log($"Asteroid destroyed, remaining: {_asteroidsRemaining}, asteroid: {data.Config.name}");
            _asteroidsRemaining--;
            if (_asteroidsRemaining <= 0) {
                StartWave();
            }
        }

        private int CalculateTotalAsteroids(AsteroidConfig config, int count) {
            int total = 0;
            int currentCount = count;
            AsteroidConfig currentConfig = config;

            while (currentConfig != null) {
                total += currentCount;
                currentCount *= currentConfig.SplitCount;
                currentConfig = currentConfig.SplitIntoConfig;
            }

            return total;
        }
    }
}