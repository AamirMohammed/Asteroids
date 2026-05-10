using TMPro;
using UnityEngine;

namespace Asteroids.UI.HUD {
    public class ScoreView : MonoBehaviour, IScoreView {
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetScore(int score) {
            _scoreText.text = score.ToString();
        }
    }
}