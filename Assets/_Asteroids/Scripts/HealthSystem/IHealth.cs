using System;

namespace Asteroids.HealthSystem {
    public interface IHealth {
        int Lives { get; }
        bool IsDead { get; }
        event Action<int> LivesChanged;
        event Action Died;
        void LoseLife();
        void Reset();
    }
}