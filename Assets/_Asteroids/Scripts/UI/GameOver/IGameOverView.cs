using System;

namespace Asteroids.UI.GameOver {
    public interface IGameOverView {
        event Action RestartPressed;
        void Show();
        void Hide();
    }
}