using UnityEngine;

namespace Asteroids.Ship {
    [CreateAssetMenu(fileName = "ShipConfig", menuName = "Asteroids/Ship Config")]
    public class ShipConfig : ScriptableObject, IShipConfig {
        [SerializeField, Range(1f, 20f)] private float _thrustForce = 5f;
        [SerializeField, Range(50f, 500f)] private float _rotationSpeed = 200f;

        public float ThrustForce => _thrustForce;
        public float RotationSpeed => _rotationSpeed;
    }
}