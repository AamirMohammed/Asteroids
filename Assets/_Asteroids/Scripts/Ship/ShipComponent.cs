using UnityEngine;

namespace Asteroids.Ship {
    public class ShipComponent : MonoBehaviour, IShip {
        [SerializeField] private ShipMovement _shipMovement;

        public void Teleport(Vector3 position) {
            _shipMovement.Teleport(position);
        }

        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}