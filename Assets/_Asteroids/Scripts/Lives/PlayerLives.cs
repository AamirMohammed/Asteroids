using System;
using Asteroids.Core;

namespace Asteroids.Lives {
    public class PlayerLives : IPlayerLives {
        private readonly IPlayerConfig _config;

        public int Lives { get; private set; }
        public bool IsOutOfLives => Lives <= 0;
        public event Action<int> LivesChanged;
        public event Action LivesDepleted;

        public PlayerLives(IPlayerConfig config) {
            _config = config;
            Lives = _config.Lives;
        }

        public void LoseLife() {
            if (IsOutOfLives) {
                return;
            }

            Lives--;
            LivesChanged?.Invoke(Lives);
            if (IsOutOfLives) {
                LivesDepleted?.Invoke();
            }
        }

        public void Reset() {
            Lives = _config.Lives;
            LivesChanged?.Invoke(Lives);
        }
    }
}