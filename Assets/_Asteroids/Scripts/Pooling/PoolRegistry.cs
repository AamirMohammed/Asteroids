using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Pooling {
    public class PoolRegistry : MonoBehaviour {
        private readonly Dictionary<string, PrefabPool> _pools = new Dictionary<string, PrefabPool>();

        private void Start() {
            foreach (PrefabPool pool in GetComponentsInChildren<PrefabPool>()) {
                _pools.Add(pool.RuntimeKey, pool);
            }
        }

        public PoolItem Get(string runtimeKey) {
            if (_pools.TryGetValue(runtimeKey, out PrefabPool pool)) {
                return pool.GetItem();
            }

            Debug.LogError($"No pool found for runtime key: {runtimeKey}");
            return null;
        }
    }
}