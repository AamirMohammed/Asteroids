using UnityEngine.AddressableAssets;

namespace Asteroids.Asteroid {
    public interface IAsteroidConfig {
        float MinSpeed { get; }
        float MaxSpeed { get; }
        int ScoreValue { get; }
        IAsteroidConfig SplitIntoConfig { get; }
        bool CanSplit { get; }
        int SplitCount { get; }
        AssetReferenceGameObject PrefabReference { get; }
    }
}