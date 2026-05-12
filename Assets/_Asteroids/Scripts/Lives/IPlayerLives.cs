using System;

namespace Asteroids.Lives {
    public interface IPlayerLives {
        int Lives { get; }
        bool IsOutOfLives { get; }
        event Action<int> LivesChanged;
        event Action LivesDepleted;
        void LoseLife();
        void Reset();
    }
}