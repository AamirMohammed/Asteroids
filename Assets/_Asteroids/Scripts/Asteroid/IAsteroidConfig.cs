namespace Asteroids.Asteroid {
    public interface IAsteroidConfig {
        float MinSpeed { get; }
        float MaxSpeed { get; }
        int SpawnCount { get; }
        int ScoreValue { get; }
    }
}