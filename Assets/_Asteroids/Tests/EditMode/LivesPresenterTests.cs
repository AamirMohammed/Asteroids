using Asteroids.Lives;
using Asteroids.UI.HUD;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class LivesPresenterTests {
        private IPlayerLives _playerLives;
        private ILivesView _view;
        private LivesPresenter _presenter;

        [SetUp]
        public void Setup() {
            _playerLives = Substitute.For<IPlayerLives>();
            _view = Substitute.For<ILivesView>();
            _presenter = new LivesPresenter(_playerLives, _view);
        }

        [TearDown]
        public void TearDown() {
            _presenter.Dispose();
        }

        [Test]
        public void Initialize_WhenCalled_SetsInitialLives() {
            _playerLives.Lives.Returns(3);
            _presenter.Initialize();
            _view.Received().SetLives(3);
        }

        [Test]
        public void LivesChanged_WhenEventFires_UpdatesView() {
            _presenter.Initialize();
            _playerLives.LivesChanged += Raise.Event<System.Action<int>>(2);
            _view.Received().SetLives(2);
        }

        [Test]
        public void Dispose_WhenCalled_UnsubscribesFromLivesChanged() {
            _presenter.Initialize();
            _presenter.Dispose();
            _playerLives.LivesChanged += Raise.Event<System.Action<int>>(2);
            _view.DidNotReceive().SetLives(2);
        }
    }
}