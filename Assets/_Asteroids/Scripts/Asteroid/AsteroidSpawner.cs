using System;
using Asteroids.Pooling;
using Asteroids.ScreenWrap;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Asteroid {
    public class AsteroidSpawner {
        private enum ScreenEdge {
            Top,
            Bottom,
            Left,
            Right
        }

        private readonly PoolRegistry _poolRegistry;
        private readonly IScreenBoundsProvider _screenBoundsProvider;
        private readonly AsteroidConfig _config;

        public AsteroidSpawner(PoolRegistry poolRegistry, IScreenBoundsProvider screenBoundsProvider,
            AsteroidConfig config) {
            _poolRegistry = poolRegistry;
            _screenBoundsProvider = screenBoundsProvider;
            _config = config;
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

            asteroid.transform.position = GetRandomEdgePosition();
            asteroid.Init(GetRandomDirection(), GetRandomSpeed());
        }

        private Vector2 GetRandomEdgePosition() {
            Vector2 bottomLeft = _screenBoundsProvider.BottomLeft;
            Vector2 topRight = _screenBoundsProvider.TopRight;

            ScreenEdge edge = (ScreenEdge)Random.Range(0, Enum.GetValues(typeof(ScreenEdge)).Length);

            switch (edge) {
                case ScreenEdge.Top:
                    return new Vector2(Random.Range(bottomLeft.x, topRight.x), topRight.y);
                case ScreenEdge.Bottom:
                    return new Vector2(Random.Range(bottomLeft.x, topRight.x), bottomLeft.y);
                case ScreenEdge.Left:
                    return new Vector2(bottomLeft.x, Random.Range(bottomLeft.y, topRight.y));
                case ScreenEdge.Right:
                    return new Vector2(topRight.x, Random.Range(bottomLeft.y, topRight.y));
                default:
                    return Vector2.zero;
            }
        }

        private Vector2 GetRandomDirection() {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        private float GetRandomSpeed() {
            return Random.Range(_config.MinSpeed, _config.MaxSpeed);
        }
    }
}