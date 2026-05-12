using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Pooling {
    public class PrefabPool : MonoBehaviour {
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        [SerializeField, Range(1, 20)] private int _defaultCapacity = 10;
        [SerializeField, Range(1, 50)] private int _maxSize = 20;

        private ObjectPool<PoolItem> _pool;
        private GameObject _prefab;
        private IObjectResolver _container;
        private readonly HashSet<PoolItem> _activeItems = new HashSet<PoolItem>();

        public object RuntimeKey => _prefabReference.RuntimeKey;
        public bool IsReady { get; private set; }


        [Inject]
        public void Construct(IObjectResolver container) {
            _container = container;
        }

        private void Start() {
            _prefabReference.LoadAssetAsync<GameObject>().Completed += OnPrefabLoaded;
        }

        private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle) {
            if (handle.Status != AsyncOperationStatus.Succeeded) {
                Debug.LogError($"Failed to load prefab for pool: {name}");
                return;
            }

            _prefab = handle.Result;

            _pool = new ObjectPool<PoolItem>(
                createFunc: Create,
                actionOnGet: Get,
                actionOnRelease: Release,
                actionOnDestroy: DestroyItem,
                defaultCapacity: _defaultCapacity,
                collectionCheck: false,
                maxSize: _maxSize
            );
            IsReady = true;
        }

        public PoolItem GetItem() {
            if (_pool == null) {
                Debug.LogError($"Pool not ready yet: {name}");
                return null;
            }

            return _pool.Get();
        }

        private PoolItem Create() {
            PoolItem item = _container.Instantiate(_prefab, transform).GetComponent<PoolItem>();
            item.Pool = _pool;
            return item;
        }

        private void Get(PoolItem item) {
            item.gameObject.SetActive(true);
            _activeItems.Add(item);
        }

        private void Release(PoolItem item) {
            _activeItems.Remove(item);
            if (item.transform.parent != transform) {
                item.transform.SetParent(transform);
            }

            item.gameObject.SetActive(false);
        }

        private void DestroyItem(PoolItem item) {
            Destroy(item.gameObject);
        }

        private void OnDestroy() {
            _prefabReference.ReleaseAsset();
        }

        public void ResetPool() {
            foreach (PoolItem item in new HashSet<PoolItem>(_activeItems)) {
                _pool.Release(item);
            }
        }
    }
}