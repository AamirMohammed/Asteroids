using Asteroids.Pooling;
using UnityEngine;

namespace Asteroids.Projectiles {
    public class Bullet : PoolItem {
        [SerializeField] private BulletConfig _config;

        private float _elapsedTime;

        private void OnEnable() {
            _elapsedTime = 0f;
        }

        private void Update() {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _config.Lifetime) {
                Pool.Release(this);
                return;
            }

            transform.Translate(Vector2.up * (_config.Speed * Time.deltaTime));
        }
    }
}