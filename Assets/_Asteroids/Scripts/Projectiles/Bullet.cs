using Asteroids.Pooling;
using UnityEngine;

namespace Asteroids.Projectiles {
    public class Bullet : PoolItem {
        [SerializeField] private BulletConfig _config;

        private BulletController _controller;

        private void Awake() {
            _controller = new BulletController(_config);
        }

        private void OnEnable() {
            _controller.Reset();
        }

        private void Update() {
            if (_controller.IsExpired(Time.deltaTime)) {
                Pool.Release(this);
                return;
            }

            transform.Translate(_controller.GetMovement(Time.deltaTime));
        }
    }
}