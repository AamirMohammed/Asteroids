using System;
using Asteroids.Randomization;
using Asteroids.ScreenWrap;
using UnityEngine;

namespace Asteroids.Asteroid {
    public class AsteroidSpawnController {
        private enum ScreenEdge {
            Top,
            Bottom,
            Left,
            Right
        }

        private readonly IScreenBoundsProvider _screenBoundsProvider;
        private readonly IAsteroidConfig _config;
        private readonly IRandomProvider _random;

        public AsteroidSpawnController(IScreenBoundsProvider screenBoundsProvider, IAsteroidConfig config,
            IRandomProvider random) {
            _screenBoundsProvider = screenBoundsProvider;
            _config = config;
            _random = random;
        }

        public Vector2 GetRandomEdgePosition() {
            Vector2 bottomLeft = _screenBoundsProvider.BottomLeft;
            Vector2 topRight = _screenBoundsProvider.TopRight;

            ScreenEdge edge = (ScreenEdge)_random.Range(0, Enum.GetValues(typeof(ScreenEdge)).Length);

            switch (edge) {
                case ScreenEdge.Top:
                    return new Vector2(_random.Range(bottomLeft.x, topRight.x), topRight.y);
                case ScreenEdge.Bottom:
                    return new Vector2(_random.Range(bottomLeft.x, topRight.x), bottomLeft.y);
                case ScreenEdge.Left:
                    return new Vector2(bottomLeft.x, _random.Range(bottomLeft.y, topRight.y));
                case ScreenEdge.Right:
                    return new Vector2(topRight.x, _random.Range(bottomLeft.y, topRight.y));
                default:
                    return Vector2.zero;
            }
        }

        public Vector2 GetRandomDirection() {
            float angle = _random.Range(0f, 360f) * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public float GetRandomSpeed() {
            return _random.Range(_config.MinSpeed, _config.MaxSpeed);
        }
    }
}