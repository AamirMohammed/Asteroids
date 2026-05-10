using Asteroids.Pooling;
using Asteroids.Projectiles;
using Asteroids.Scoring;
using UnityEngine;
using VContainer;

namespace Asteroids.Asteroid {
    public class Asteroid : PoolItem {
        [SerializeField] private Rigidbody2D _rigidbody;

        private IAsteroidConfig _config;
        private IScoreSystem _scoreSystem;

        [Inject]
        public void Construct(IAsteroidConfig config, IScoreSystem scoreSystem) {
            _config = config;
            _scoreSystem = scoreSystem;
        }
    
        public void Init(Vector2 direction, float speed) {
            _rigidbody.linearVelocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (!other.TryGetComponent(out Bullet bullet)) {
                return;
            }

            bullet.Pool.Release(bullet);
            _scoreSystem.AddScore(_config.ScoreValue);
            Pool.Release(this);
        }
    }
}