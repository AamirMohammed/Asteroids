namespace Asteroids.Asteroid {
    public interface IAsteroidSpawnService {
        void SpawnWave(int count, IAsteroidConfig  config);
    }
}