using UnityEngine;

namespace Asteroids.Projectiles {
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Asteroids/Bullet Config")]
    public class BulletConfig : ScriptableObject {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifetime = 3f;

        public float Speed => _speed;
        public float Lifetime => _lifetime;
    }
}