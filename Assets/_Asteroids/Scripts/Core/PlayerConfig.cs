using UnityEngine;

namespace Asteroids.Core {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/Player Config")]
    public class PlayerConfig : ScriptableObject, IPlayerConfig {
        [SerializeField, Range(1, 5)] private int _lives = 3;
        public int Lives => _lives;
    }
}