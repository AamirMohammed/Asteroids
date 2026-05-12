using Asteroids.Asteroid;
using Asteroids.Lives;
using UnityEngine;
using VContainer;

namespace Asteroids.Ship {
    public class ShipCollision : MonoBehaviour {
        private IPlayerLives _playerLives;

        [Inject]
        public void Construct(IPlayerLives playerLives) {
            _playerLives = playerLives;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.attachedRigidbody == null) {
                return;
            }

            if (!other.attachedRigidbody.TryGetComponent(out AsteroidComponent asteroidComponent)) {
                return;
            }

            _playerLives.LoseLife();
        }
    }
}