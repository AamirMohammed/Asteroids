using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Asteroid {
    [CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroids/Asteroid Config")]
    public class AsteroidConfig : ScriptableObject, IAsteroidConfig {
        [SerializeField, Range(1f, 10f)] private float _minSpeed = 1f;
        [SerializeField, Range(1f, 10f)] private float _maxSpeed = 3f;
        [SerializeField, Range(1, 10)] private int _spawnCount = 4;
        [SerializeField] private AssetReferenceGameObject _asteroidReference;

        public float MinSpeed => _minSpeed;
        public float MaxSpeed => _maxSpeed;
        public int SpawnCount => _spawnCount;
        public AssetReferenceGameObject AsteroidReference => _asteroidReference;
    }
}