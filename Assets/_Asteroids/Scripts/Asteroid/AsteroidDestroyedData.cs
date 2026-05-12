using UnityEngine;

namespace Asteroids.Asteroid {
    public readonly struct AsteroidDestroyedData {
        public IAsteroidConfig Config { get; }
        public Vector2 Position { get; }

        public AsteroidDestroyedData(IAsteroidConfig config, Vector2 position) {
            Config = config;
            Position = position;
        }
    }
}