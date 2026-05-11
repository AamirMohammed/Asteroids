using Asteroids.Pooling;
using UnityEngine;

namespace Asteroids.Projectiles {
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletComponent : PoolItem {
        [SerializeField] private BulletConfig _config;

        private Bullet _bullet;
        private Rigidbody2D _rigidbody;

        private void Awake() {
            _bullet = new Bullet(_config);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector3 position, Quaternion rotation) {
            transform.SetPositionAndRotation(position, rotation);
            _rigidbody.position = position;
        }

        private void OnEnable() {
            _bullet.Reset();
        }

        private void FixedUpdate() {
            if (_bullet.IsExpired(Time.fixedDeltaTime)) {
                Pool.Release(this);
                return;
            }

            _rigidbody.MovePosition(_rigidbody.position +
                                    _bullet.GetMovement(Time.fixedDeltaTime, _rigidbody.transform.up));
        }
    }
}