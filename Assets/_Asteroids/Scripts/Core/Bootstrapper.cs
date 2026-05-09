using Asteroids.Asteroid;
using Asteroids.Pooling;
using VContainer.Unity;

namespace Asteroids.Core {
    public class Bootstrapper : IStartable {
        private readonly PoolRegistry _poolRegistry;
        private readonly AsteroidSpawner _asteroidSpawner;

        public Bootstrapper(PoolRegistry poolRegistry, AsteroidSpawner asteroidSpawner) {
            _poolRegistry = poolRegistry;
            _asteroidSpawner = asteroidSpawner;
        }

        public void Start() {
            _poolRegistry.OnReady += OnPoolsReady;
        }

        private void OnPoolsReady() {
            _poolRegistry.OnReady -= OnPoolsReady;
            _asteroidSpawner.SpawnWave();
        }
    }
}