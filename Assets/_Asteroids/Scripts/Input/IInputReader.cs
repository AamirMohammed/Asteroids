using System;

namespace Asteroids.Input {
    public interface IInputReader {
        public event Action ShootPressed;
        public event Action<float> RotateInput;
        public event Action<bool> ThrustInput;
    }
}