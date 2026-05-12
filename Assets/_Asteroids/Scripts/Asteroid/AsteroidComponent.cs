using Asteroids.EventChannels;
using Asteroids.Pooling;
using Asteroids.Projectiles;
using UnityEngine;
using VContainer;

namespace Asteroids.Asteroid {
    public class AsteroidComponent : PoolItem {
        [SerializeField] private Rigidbody2D _rigidbody;

        private IAsteroidConfig _config;
        private IEventChannel<AsteroidDestroyedData> _destroyedChannel;

        [Inject]
        public void Construct(IEventChannel<AsteroidDestroyedData> destroyedChannel) {
            _destroyedChannel = destroyedChannel;
        }

        public void Init(Vector2 direction, float speed, IAsteroidConfig config) {
            _rigidbody.linearVelocity = direction * speed;
            _config = config;
        }


        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.TryGetComponent(out BulletComponent bullet)) {
                return;
            }

            _destroyedChannel.Publish(new AsteroidDestroyedData(_config, _rigidbody.position));

            bullet.Pool.Release(bullet);
            Pool.Release(this);
        }
    }
}