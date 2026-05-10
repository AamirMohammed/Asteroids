using Asteroids.Asteroid;
using Asteroids.HealthSystem;
using UnityEngine;
using VContainer;

namespace Asteroids.Ship {
    public class ShipCollision : MonoBehaviour {
        private IHealth _health;

        [Inject]
        public void Construct(IHealth health) {
            _health = health;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.attachedRigidbody == null) {
                return;
            }

            if (!other.attachedRigidbody.TryGetComponent(out AsteroidComponent asteroidComponent)) {
                return;
            }

            _health.LoseLife();
        }
    }
}