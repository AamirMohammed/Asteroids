using System;
using Asteroids.Scoring;
using VContainer.Unity;

namespace Asteroids.UI.HUD {
    public class ScorePresenter : IDisposable, IInitializable {
        private readonly IScoreService _scoreService;
        private readonly IScoreView _view;

        public ScorePresenter(IScoreService scoreService, IScoreView view) {
            _scoreService = scoreService;
            _view = view;
        }

        public void Initialize() {
            _scoreService.ScoreChanged += OnScoreChanged;
            _view.SetScore(_scoreService.Score);
        }

        private void OnScoreChanged(int score) {
            _view.SetScore(score);
        }

        public void Dispose() {
            _scoreService.ScoreChanged -= OnScoreChanged;
        }
    }
}