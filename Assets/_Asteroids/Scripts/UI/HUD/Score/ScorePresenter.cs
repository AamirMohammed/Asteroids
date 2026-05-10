using System;
using Asteroids.Scoring;
using VContainer.Unity;

namespace Asteroids.UI.HUD {
    public class ScorePresenter : IDisposable, IInitializable {
        private readonly IScoreSystem _scoreSystem;
        private readonly IScoreView _view;

        public ScorePresenter(IScoreSystem scoreSystem, IScoreView view) {
            _scoreSystem = scoreSystem;
            _view = view;
        }

        public void Initialize() {
            _scoreSystem.ScoreChanged += OnScoreChanged;
            _view.SetScore(_scoreSystem.Score);
        }

        private void OnScoreChanged(int score) {
            _view.SetScore(score);
        }

        public void Dispose() {
            _scoreSystem.ScoreChanged -= OnScoreChanged;
        }
    }
}