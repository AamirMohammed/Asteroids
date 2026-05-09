using UnityEngine;

namespace Asteroids.Randomization {
    public class UnityRandomProvider : IRandomProvider {
        public float Range(float min, float max) {
            return Random.Range(min, max);
        }

        public int Range(int min, int max) {
            return Random.Range(min, max);
        }
    }
}