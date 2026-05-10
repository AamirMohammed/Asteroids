using UnityEngine;

namespace Asteroids.WaveSystem {
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Asteroids/Wave Config")]
    public class WaveConfig : ScriptableObject {
        [SerializeField] private int[] _asteroidCountPerWave = { 4, 6, 8, 10, 11 };

        public int GetAsteroidCount(int wave) {
            int index = wave - 1;
            if (index < _asteroidCountPerWave.Length) {
                return _asteroidCountPerWave[index];
            }

            return _asteroidCountPerWave[^1];
        }
    }
}