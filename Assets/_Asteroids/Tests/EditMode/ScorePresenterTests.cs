using Asteroids.Scoring;
using Asteroids.UI.HUD;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class ScorePresenterTests {
        private IScoreService _scoreService;
        private IScoreView _view;
        private ScorePresenter _presenter;

        [SetUp]
        public void Setup() {
            _scoreService = Substitute.For<IScoreService>();
            _view = Substitute.For<IScoreView>();
            _presenter = new ScorePresenter(_scoreService, _view);
        }

        [TearDown]
        public void TearDown() {
            _presenter.Dispose();
        }

        [Test]
        public void Initialize_WhenCalled_SetsInitialScore() {
            _scoreService.Score.Returns(0);
            _presenter.Initialize();
            _view.Received().SetScore(0);
        }

        [Test]
        public void ScoreChanged_WhenEventFires_UpdatesView() {
            _presenter.Initialize();
            _scoreService.ScoreChanged += Raise.Event<System.Action<int>>(42);
            _view.Received().SetScore(42);
        }

        [Test]
        public void Dispose_WhenCalled_UnsubscribesFromScoreChanged() {
            _presenter.Initialize();
            _presenter.Dispose();
            _scoreService.ScoreChanged += Raise.Event<System.Action<int>>(42);
            _view.DidNotReceive().SetScore(42);
        }
    }
}