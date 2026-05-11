using Asteroids.Pooling;
using Asteroids.Projectiles;
using UnityEngine;
using VContainer;

namespace Asteroids.Asteroid {
    public class AsteroidComponent : PoolItem {
        [SerializeField] private Rigidbody2D _rigidbody;

        private IAsteroidConfig _config;
        private AsteroidDestroyedChannel _destroyedChannel;

        [Inject]
        public void Construct(AsteroidDestroyedChannel destroyedChannel) {
            _destroyedChannel = destroyedChannel;
        }

        public void Init(Vector2 direction, float speed, IAsteroidConfig config) {
            _rigidbody.linearVelocity = direction * speed;
            _config = config;
        }


        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.TryGetComponent(out Bullet bullet)) {
                return;
            }

            bullet.Pool.Release(bullet);
            Pool.Release(this);
            _destroyedChannel.Publish(new AsteroidDestroyedData {
                Config = _config,
                Position = _rigidbody.position
            });
        }
    }
}