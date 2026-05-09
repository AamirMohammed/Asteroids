using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Pooling {
    public class PoolRegistry : MonoBehaviour {
        private readonly Dictionary<string, PrefabPool> _pools = new Dictionary<string, PrefabPool>();

        private void Start() {
            foreach (PrefabPool pool in GetComponentsInChildren<PrefabPool>()) {
                _pools.Add(pool.RuntimeKey, pool);
            }
        }

        public T Get<T>(AssetReference assetReference) where T : PoolItem {
            string runtimeKey = assetReference.RuntimeKey.ToString();
            if (_pools.TryGetValue(runtimeKey, out PrefabPool pool)) {
                return pool.GetItem() as T;
            }

            Debug.LogError($"No pool found for runtime key: {runtimeKey}");
            return null;
        }
    }
}