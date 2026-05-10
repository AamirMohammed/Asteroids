using System;
using Asteroids.Core;
using Asteroids.HealthSystem;
using VContainer.Unity;

namespace Asteroids.UI.GameOver {
    public class GameOverPresenter : IInitializable, IDisposable {
        private readonly IHealth _health;
        private readonly IGameOverView _view;
        private readonly GameStateService _gameStateService;

        public GameOverPresenter(IHealth health, IGameOverView view, GameStateService gameStateService) {
            _health = health;
            _view = view;
            _gameStateService = gameStateService;
        }

        public void Initialize() {
            _health.Died += OnDied;
            _view.RestartPressed += OnRestartPressed;
            _view.Hide();
        }

        public void Dispose() {
            _health.Died -= OnDied;
            _view.RestartPressed -= OnRestartPressed;
        }

        private void OnDied() {
            _view.Show();
        }

        private void OnRestartPressed() {
            _view.Hide();
            _gameStateService.Restart();
        }
    }
}