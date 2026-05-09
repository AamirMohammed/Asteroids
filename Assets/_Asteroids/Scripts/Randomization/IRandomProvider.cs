namespace Asteroids.Randomization {
    public interface IRandomProvider {
        float Range(float min, float max);
        int Range(int min, int max);
    }
}