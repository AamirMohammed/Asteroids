using System;
using Asteroids.HealthSystem;
using VContainer.Unity;

namespace Asteroids.UI.HUD {
    public class LivesPresenter : IDisposable, IInitializable {
        private readonly IHealth _health;
        private readonly ILivesView _view;

        public LivesPresenter(IHealth health, ILivesView view) {
            _health = health;
            _view = view;
        }

        public void Initialize() {
            _health.LivesChanged += OnLivesChanged;
            _view.SetLives(_health.Lives);
        }

        private void OnLivesChanged(int lives) {
            _view.SetLives(lives);
        }

        public void Dispose() {
            _health.LivesChanged -= OnLivesChanged;
        }
    }
}