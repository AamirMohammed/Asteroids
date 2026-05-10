using System;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.UI.GameOver {
    public class GameOverView : MonoBehaviour, IGameOverView {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _restartButton;

        public event Action RestartPressed;

        private void OnEnable() {
            _restartButton.onClick.AddListener(OnRestartPressed);
        }

        private void OnDisable() {
            _restartButton.onClick.RemoveListener(OnRestartPressed);
        }

        public void Show() {
            _panel.SetActive(true);
        }

        public void Hide() {
            _panel.SetActive(false);
        }

        private void OnRestartPressed() {
            RestartPressed?.Invoke();
        }
    }
}