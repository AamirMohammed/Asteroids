using System;
using Asteroids.Asteroid;
using Asteroids.EventChannels;
using VContainer.Unity;

namespace Asteroids.Scoring {
    public class ScoreService : IScoreService, IInitializable, IDisposable {
        private readonly IReadOnlyEventChannel<AsteroidDestroyedData> _asteroidDestroyedChannel;

        public int Score { get; private set; }
        public event Action<int> ScoreChanged;

        public ScoreService(IReadOnlyEventChannel<AsteroidDestroyedData> asteroidDestroyedChannel) {
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

        public void Reset() {
            Score = 0;
            ScoreChanged?.Invoke(Score);
        }
    }
}