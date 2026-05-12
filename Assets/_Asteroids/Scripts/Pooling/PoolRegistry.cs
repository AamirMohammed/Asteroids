using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Pooling {
    public class PoolRegistry : MonoBehaviour, IPoolRegistry {
        public event Action Ready;
        public bool IsReady { get; private set; }

        private readonly Dictionary<string, PrefabPool> _pools = new Dictionary<string, PrefabPool>();

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
            Ready?.Invoke();
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
            string runtimeKey = assetReference.RuntimeKey.ToString();
            if (_pools.TryGetValue(runtimeKey, out PrefabPool pool)) {
                return pool.GetItem() as T;
            }

            Debug.LogError($"No pool found for runtime key: {runtimeKey}");
            return null;
        }

        public void ResetAllPools() {
            foreach (PrefabPool pool in _pools.Values) {
                pool.ResetPool();
            }
        }
    }
}