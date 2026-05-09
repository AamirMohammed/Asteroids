using Asteroids.Pooling;
using UnityEngine;

namespace Asteroids.Asteroid {
    public class Asteroid : PoolItem {
        [SerializeField] private Rigidbody2D _rigidbody;

        public void Init(Vector2 direction, float speed) {
            _rigidbody.linearVelocity = direction * speed;
        }
    }
}