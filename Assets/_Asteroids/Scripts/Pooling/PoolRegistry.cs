using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Asteroids.Pooling {
    public class PoolRegistry : MonoBehaviour {
        public event Action OnReady;

        private readonly Dictionary<string, PrefabPool> _pools = new Dictionary<string, PrefabPool>();

        private IEnumerator Start() {
            PrefabPool[] pools = GetComponentsInChildren<PrefabPool>();
            foreach (PrefabPool pool in pools) {
                _pools.Add(pool.RuntimeKey, pool);
            }

            yield return new WaitUntil(AllPoolsReady);
            OnReady?.Invoke();
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
    }
}