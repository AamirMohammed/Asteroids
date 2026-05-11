using UnityEngine;

namespace Asteroids.Ship {
    public interface IShip {
        void Teleport(Vector3 position);
        void Show();
        void Hide();
    }
}