using UnityEngine;

namespace Asteroids.Projectiles {
    public class BulletController {
        private readonly IBulletConfig _config;
        private float _elapsedTime;

        public BulletController(IBulletConfig config) {
            _config = config;
        }

        public void Reset() {
            _elapsedTime = 0f;
        }

        public bool IsExpired(float deltaTime) {
            _elapsedTime += deltaTime;
            return _elapsedTime >= _config.Lifetime;
        }

        public Vector2 GetMovement(float deltaTime) {
            return Vector2.up * (_config.Speed * deltaTime);
        }
    }
}