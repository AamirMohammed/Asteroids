using System;
using UnityEngine.AddressableAssets;

namespace Asteroids.Asteroid {
    [Serializable]
    public class AsteroidPrefabEntry {
        public AsteroidConfig Config;
        public AssetReferenceGameObject PrefabReference;
    }
}