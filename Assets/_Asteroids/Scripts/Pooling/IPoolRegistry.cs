using UnityEngine.AddressableAssets;

namespace Asteroids.Pooling {
    public interface IPoolRegistry {
        bool IsReady { get; }
        void Initialize();
        void ResetAllPools();
        T Get<T>(AssetReference assetReference) where T : PoolItem;
    }
}