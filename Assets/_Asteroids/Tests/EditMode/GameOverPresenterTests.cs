using Asteroids.Core;
using Asteroids.HealthSystem;
using Asteroids.UI.GameOver;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class GameOverPresenterTests {
        private IHealth _health;
        private IGameOverView _view;
        private IGameStateService _gameStateService;
        private GameOverPresenter _presenter;

        [SetUp]
        public void Setup() {
            _health = Substitute.For<IHealth>();
            _view = Substitute.For<IGameOverView>();
            _gameStateService = Substitute.For<IGameStateService>();
            _presenter = new GameOverPresenter(_health, _view, _gameStateService);
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
        public void OnDied_WhenHealthDies_ShowsView() {
            _health.Died += Raise.Event<System.Action>();

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