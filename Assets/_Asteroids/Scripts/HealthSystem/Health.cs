using System;
using Asteroids.Core;

namespace Asteroids.HealthSystem {
    public class Health : IHealth {
        private readonly IPlayerConfig _config;

        public int Lives { get; private set; }
        public bool IsDead => Lives <= 0;
        public event Action<int> LivesChanged;
        public event Action Died;

        public Health(IPlayerConfig config) {
            _config = config;
            Lives = _config.Lives;
        }

        public void LoseLife() {
            if (IsDead) {
                return;
            }

            Lives--;
            LivesChanged?.Invoke(Lives);
            if (IsDead) {
                Died?.Invoke();
            }
        }

        public void Reset() {
            Lives = _config.Lives;
            LivesChanged?.Invoke(Lives);
        }
    }
}