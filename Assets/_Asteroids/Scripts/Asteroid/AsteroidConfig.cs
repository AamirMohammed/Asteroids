using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Asteroid {
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroids/Asteroid Config")]
    public class AsteroidConfig : ScriptableObject, IAsteroidConfig {
        [SerializeField] private AssetReferenceGameObject _asteroidReference;
        [SerializeField] private AsteroidConfig _splitIntoConfig;
        [SerializeField, Range(1f, 10f)] private float _minSpeed = 1f;
        [SerializeField, Range(1f, 10f)] private float _maxSpeed = 3f;
        [SerializeField, Range(10, 200)] private int _scoreValue = 20;
        [SerializeField, Range(0, 3)] private int _splitCount = 2;

        public AssetReferenceGameObject AsteroidReference => _asteroidReference;
        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;
        public int ScoreValue => _scoreValue;
        public int SplitCount => _splitCount;
        public AsteroidConfig SplitIntoConfig => _splitIntoConfig;
        public bool CanSplit => _splitCount > 0 && _splitIntoConfig != null;
    }
}