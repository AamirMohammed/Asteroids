using TMPro;
using UnityEngine;

namespace Asteroids.UI.HUD {
    public class LivesView : MonoBehaviour, ILivesView {
        [SerializeField] private TextMeshProUGUI _livesText;

        public void SetLives(int lives) {
            _livesText.text = new string('▲', lives);
        }
    }
}