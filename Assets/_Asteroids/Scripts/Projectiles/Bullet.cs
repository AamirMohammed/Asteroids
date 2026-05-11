using Asteroids.Pooling;
using UnityEngine;

namespace Asteroids.Projectiles {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : PoolItem {
        [SerializeField] private BulletConfig _config;

        private BulletController _controller;
        private Rigidbody2D _rigidbody;

        private void Awake() {
            _controller = new BulletController(_config);
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialize(Vector3 position, Quaternion rotation) {
            transform.SetPositionAndRotation(position, rotation);
            _rigidbody.position = position;
        }

        private void OnEnable() {
            _controller.Reset();
        }

        private void FixedUpdate() {
            if (_controller.IsExpired(Time.fixedDeltaTime)) {
                Pool.Release(this);
                return;
            }

            _rigidbody.MovePosition(_rigidbody.position +
                                    _controller.GetMovement(Time.fixedDeltaTime, _rigidbody.transform.up));
        }
    }
}