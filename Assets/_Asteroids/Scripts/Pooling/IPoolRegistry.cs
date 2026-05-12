namespace Asteroids.Pooling {
    public interface IPoolRegistry {
        bool IsReady { get; }
        void Initialize();
        void ResetAllPools();
    }
}