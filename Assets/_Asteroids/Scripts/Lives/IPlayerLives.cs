using System;

namespace Asteroids.Lives {
    public interface IPlayerLives {
        int Lives { get; }
        bool IsDead { get; }
        event Action<int> LivesChanged;
        event Action Died;
        void LoseLife();
        void Reset();
    }
}