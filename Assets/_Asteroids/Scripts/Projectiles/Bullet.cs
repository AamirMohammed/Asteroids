using UnityEngine;

namespace Asteroids.Projectiles {
    public class Bullet {
        private readonly IBulletConfig _config;
        private float _elapsedTime;

        public Bullet(IBulletConfig config) {
            _config = config;
        }

        public void Reset() {
            _elapsedTime = 0f;
        }

        public bool IsExpired(float deltaTime) {
            _elapsedTime += deltaTime;
            return _elapsedTime >= _config.Lifetime;
        }

        public Vector2 GetMovement(float deltaTime, Vector2 direction) {
            return direction * (_config.Speed * deltaTime);
        }
    }
}