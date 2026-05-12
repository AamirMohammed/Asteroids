using System;
using Asteroids.Lives;
using VContainer.Unity;

namespace Asteroids.UI.HUD {
    public class LivesPresenter : IInitializable, IDisposable {
        private readonly IPlayerLives _playerLives;
        private readonly ILivesView _view;

        public LivesPresenter(IPlayerLives playerLives, ILivesView view) {
            _playerLives = playerLives;
            _view = view;
        }

        public void Initialize() {
            _playerLives.LivesChanged += OnLivesChanged;
            _view.SetLives(_playerLives.Lives);
        }

        private void OnLivesChanged(int lives) {
            _view.SetLives(lives);
        }

        public void Dispose() {
            _playerLives.LivesChanged -= OnLivesChanged;
        }
    }
}