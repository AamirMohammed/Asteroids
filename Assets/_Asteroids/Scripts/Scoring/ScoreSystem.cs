using System;
using Asteroids.Asteroid;
using VContainer.Unity;

namespace Asteroids.Scoring {
    public class ScoreSystem : IScoreSystem, IInitializable, IDisposable {
        private readonly AsteroidDestroyedChannel _asteroidDestroyedChannel;

        public int Score { get; private set; }
        public event Action<int> ScoreChanged;

        public ScoreSystem(AsteroidDestroyedChannel asteroidDestroyedChannel) {
            _asteroidDestroyedChannel = asteroidDestroyedChannel;
        }

        public void Initialize() {
            _asteroidDestroyedChannel.Published += OnAsteroidDestroyed;
        }

        public void Dispose() {
            _asteroidDestroyedChannel.Published -= OnAsteroidDestroyed;
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedData data) {
            AddScore(data.Config.ScoreValue);
        }

        public void AddScore(int points) {
            Score += points;
            ScoreChanged?.Invoke(Score);
        }
    }
}