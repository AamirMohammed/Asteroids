using Asteroids.Pooling;
using Asteroids.Randomization;
using Asteroids.ScreenWrap;

namespace Asteroids.Asteroid {
    public class AsteroidSpawner {
        private readonly PoolRegistry _poolRegistry;
        private readonly AsteroidConfig _config;
        private readonly AsteroidSpawnController _controller;

        public AsteroidSpawner(PoolRegistry poolRegistry, IScreenBoundsProvider screenBoundsProvider,
            AsteroidConfig config, IRandomProvider random) {
            _poolRegistry = poolRegistry;
            _config = config;
            _controller = new AsteroidSpawnController(screenBoundsProvider, config, random);
        }

        public void SpawnWave() {
            for (int i = 0; i < _config.SpawnCount; i++) {
                SpawnAsteroid();
            }
        }

        private void SpawnAsteroid() {
            Asteroid asteroid = _poolRegistry.Get<Asteroid>(_config.AsteroidReference);
            if (asteroid == null) {
                return;
            }

            asteroid.transform.position = _controller.GetRandomEdgePosition();
            asteroid.Init(_controller.GetRandomDirection(), _controller.GetRandomSpeed());
        }
    }
}