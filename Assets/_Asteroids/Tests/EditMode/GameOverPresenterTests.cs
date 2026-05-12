using Asteroids.Core;
using Asteroids.Lives;
using Asteroids.UI.GameOver;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class GameOverPresenterTests {
        private IPlayerLives _playerLives;
        private IGameOverView _view;
        private IGameStateService _gameStateService;
        private GameOverPresenter _presenter;

        [SetUp]
        public void Setup() {
            _playerLives = Substitute.For<IPlayerLives>();
            _view = Substitute.For<IGameOverView>();
            _gameStateService = Substitute.For<IGameStateService>();
            _presenter = new GameOverPresenter(_playerLives, _view, _gameStateService);
            _presenter.Initialize();
        }

        [TearDown]
        public void TearDown() {
            _presenter.Dispose();
        }

        [Test]
        public void Initialize_Always_HidesView() {
            _view.Received().Hide();
        }

        [Test]
        public void OnDied_WhenLivesDepleted_ShowsView() {
            _playerLives.LivesDepleted += Raise.Event<System.Action>();

            _view.Received().Show();
        }

        [Test]
        public void OnRestartPressed_WhenRestartPressed_HidesView() {
            _view.RestartPressed += Raise.Event<System.Action>();

            _view.Received().Hide();
        }

        [Test]
        public void OnRestartPressed_WhenRestartPressed_CallsRestart() {
            _view.RestartPressed += Raise.Event<System.Action>();

            _gameStateService.Received().Restart();
        }
    }
}