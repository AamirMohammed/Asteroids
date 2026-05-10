using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Asteroid {
    public class AsteroidSpawner : MonoBehaviour, IAsteroidSpawner {
        [SerializeField] private List<AsteroidPrefabEntry> _prefabEntries;

        public IReadOnlyList<AsteroidPrefabEntry> PrefabEntries => _prefabEntries;
    }
}