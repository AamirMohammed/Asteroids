using System;
using Asteroids.Core;
using Asteroids.Lives;
using VContainer.Unity;

namespace Asteroids.UI.GameOver {
    public class GameOverPresenter : IInitializable, IDisposable {
        private readonly IPlayerLives _playerLives;
        private readonly IGameOverView _view;
        private readonly IGameStateService _gameStateService;

        public GameOverPresenter(IPlayerLives playerLives, IGameOverView view, IGameStateService gameStateService) {
            _playerLives = playerLives;
            _view = view;
            _gameStateService = gameStateService;
        }

        public void Initialize() {
            _playerLives.Died += OnDied;
            _view.RestartPressed += OnRestartPressed;
            _view.Hide();
        }

        public void Dispose() {
            _playerLives.Died -= OnDied;
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