using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Pooling {
    public class PoolRegistry : MonoBehaviour, IPoolRegistry {
        public bool IsReady { get; private set; }

        private readonly Dictionary<object, PrefabPool> _pools = new Dictionary<object, PrefabPool>();

        public void Initialize() {
            PrefabPool[] pools = GetComponentsInChildren<PrefabPool>();
            foreach (PrefabPool pool in pools) {
                if (!_pools.TryAdd(pool.RuntimeKey, pool)) {
                    Debug.LogError($"Duplicate pool runtime key: {pool.RuntimeKey}", pool);
                }
            }

            StartCoroutine(WaitForPools());
        }

        private IEnumerator WaitForPools() {
            yield return new WaitUntil(AllPoolsReady);
            IsReady = true;
        }

        private bool AllPoolsReady() {
            foreach (PrefabPool pool in _pools.Values) {
                if (!pool.IsReady) {
                    return false;
                }
            }

            return true;
        }

        public T Get<T>(AssetReference assetReference) where T : PoolItem {
            if (_pools.TryGetValue(assetReference.RuntimeKey, out PrefabPool pool)) {
                return pool.GetItem() as T;
            }

            Debug.LogError($"No pool found for runtime key: {assetReference.RuntimeKey}");
            return null;
        }

        public void ResetAllPools() {
            foreach (PrefabPool pool in _pools.Values) {
                pool.ResetPool();
            }
        }
    }
}