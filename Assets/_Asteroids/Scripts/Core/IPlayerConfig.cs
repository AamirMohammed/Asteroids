namespace Asteroids.Core {
    public interface IPlayerConfig {
        int Lives { get; }
        float RespawnDelay { get; }
    }
}