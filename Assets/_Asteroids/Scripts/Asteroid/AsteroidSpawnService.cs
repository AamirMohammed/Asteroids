using System;
using Asteroids.EventChannels;
using Asteroids.Pooling;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Asteroid {
    public class AsteroidSpawnService : IInitializable, IDisposable, IAsteroidSpawnService {
        private readonly IPoolRegistry _poolRegistry;
        private readonly IReadOnlyEventChannel<AsteroidDestroyedData> _destroyedChannel;
        private readonly AsteroidSpawnController _spawnController;

        public AsteroidSpawnService(IPoolRegistry poolRegistry,
            IReadOnlyEventChannel<AsteroidDestroyedData> destroyedChannel,
            AsteroidSpawnController spawnController) {
            _poolRegistry = poolRegistry;
            _destroyedChannel = destroyedChannel;
            _spawnController = spawnController;
        }

        public void Initialize() {
            _destroyedChannel.Published += OnAsteroidDestroyed;
        }

        public void Dispose() {
            _destroyedChannel.Published -= OnAsteroidDestroyed;
        }

        public void SpawnWave(int count, IAsteroidConfig config) {
            for (int i = 0; i < count; i++) {
                SpawnAsteroid(config, _spawnController.GetRandomEdgePosition());
            }
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedData data) {
            if (!data.Config.CanSplit) {
                return;
            }

            for (int i = 0; i < data.Config.SplitCount; i++) {
                SpawnAsteroid(data.Config.SplitIntoConfig, data.Position);
            }
        }

        private void SpawnAsteroid(IAsteroidConfig config, Vector2 position) {
            AsteroidComponent asteroidComponent = _poolRegistry.Get<AsteroidComponent>(config.PrefabReference);
            if (asteroidComponent == null) {
                return;
            }

            asteroidComponent.transform.position = position;
            asteroidComponent.Init(
                _spawnController.GetRandomDirection(),
                _spawnController.GetRandomSpeed(config.MinSpeed, config.MaxSpeed),
                config);
            asteroidComponent.gameObject.SetActive(true);
        }
    }
}