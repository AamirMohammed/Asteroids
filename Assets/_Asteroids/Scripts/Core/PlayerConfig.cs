using UnityEngine;

namespace Asteroids.Core {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/Player Config")]
    public class PlayerConfig : ScriptableObject, IPlayerConfig {
        [SerializeField, Range(1, 5)] private int _lives = 3;
        [SerializeField, Range(1, 5)] private float _respawnDelay = 2;
        public int Lives => _lives;
        public float RespawnDelay => _respawnDelay;
    }
}