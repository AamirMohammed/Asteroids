using System;
using Asteroids.Pooling;
using Asteroids.Randomization;
using Asteroids.ScreenWrap;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer.Unity;

namespace Asteroids.Asteroid {
    public class AsteroidSpawnService : IInitializable, IDisposable, IAsteroidSpawnService {
        private readonly PoolRegistry _poolRegistry;
        private readonly IAsteroidSpawner _asteroidSpawner;
        private readonly AsteroidDestroyedChannel _destroyedChannel;
        private readonly AsteroidSpawnController _spawnController;

        public AsteroidSpawnService(PoolRegistry poolRegistry, IAsteroidSpawner asteroidSpawner,
            IScreenBoundsProvider screenBoundsProvider, AsteroidDestroyedChannel destroyedChannel,
            IRandomProvider random) {
            _poolRegistry = poolRegistry;
            _asteroidSpawner = asteroidSpawner;
            _destroyedChannel = destroyedChannel;
            _spawnController = new AsteroidSpawnController(screenBoundsProvider, random);
        }

        public void Initialize() {
            _destroyedChannel.Published += OnAsteroidDestroyed;
        }

        public void Dispose() {
            _destroyedChannel.Published -= OnAsteroidDestroyed;
        }

        public void SpawnWave(int count, IAsteroidConfig config) {
            AssetReferenceGameObject prefab = GetPrefabReference(config);
            if (prefab == null) {
                return;
            }

            for (int i = 0; i < count; i++) {
                SpawnAsteroid(prefab, config, _spawnController.GetRandomEdgePosition());
            }
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedData data) {
            if (!data.Config.CanSplit) {
                return;
            }

            AssetReferenceGameObject prefab = GetPrefabReference(data.Config.SplitIntoConfig);
            if (prefab == null) {
                return;
            }

            for (int i = 0; i < data.Config.SplitCount; i++) {
                SpawnAsteroid(prefab, data.Config.SplitIntoConfig, data.Position);
            }
        }

        private void SpawnAsteroid(AssetReferenceGameObject prefabReference, IAsteroidConfig config, Vector2 position) {
            AsteroidComponent asteroidComponent = _poolRegistry.Get<AsteroidComponent>(prefabReference);
            if (asteroidComponent == null) {
                return;
            }

            asteroidComponent.transform.position = position;
            asteroidComponent.Init(
                _spawnController.GetRandomDirection(),
                _spawnController.GetRandomSpeed(config.MinSpeed, config.MaxSpeed),
                config as AsteroidConfig);
            asteroidComponent.gameObject.SetActive(true);
        }

        private AssetReferenceGameObject GetPrefabReference(IAsteroidConfig config) {
            foreach (AsteroidPrefabEntry entry in _asteroidSpawner.PrefabEntries) {
                if (ReferenceEquals(entry.Config, config)) {
                    return entry.PrefabReference;
                }
            }

            Debug.LogError($"No prefab found for config: {config}");
            return null;
        }
    }
}