using UnityEngine;

namespace Asteroids.Projectiles {
    public class Bullet : MonoBehaviour {
        [SerializeField] private BulletConfig _config;

        private void Update() {
            transform.Translate(Vector2.up * (_config.Speed * Time.deltaTime));
        }
    }
}