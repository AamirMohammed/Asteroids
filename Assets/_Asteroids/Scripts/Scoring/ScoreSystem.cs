using System;

namespace Asteroids.Scoring {
    public class ScoreSystem : IScoreSystem {
        public int Score { get; private set; }
        public event Action<int> ScoreChanged;

        public void AddScore(int points) {
            Score += points;
            ScoreChanged?.Invoke(Score);
        }
    }
}