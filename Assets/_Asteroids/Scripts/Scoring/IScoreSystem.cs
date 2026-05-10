using System;

namespace Asteroids.Scoring {
    public interface IScoreSystem {
        int Score { get; }
        event Action<int> ScoreChanged;
        void AddScore(int points);
        void Reset();
    }
}