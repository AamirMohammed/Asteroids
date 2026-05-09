using UnityEngine;
using VContainer;

namespace Asteroids.ScreenWrap {
    [RequireComponent(typeof(Rigidbody2D))]
    public class ScreenWrapper : MonoBehaviour {
        [SerializeField] private Collider2D _collider;

        private Rigidbody2D _rigidbody;
        private ScreenWrapCalculator _screenWrapCalculator;
        private float _radius;

        [Inject]
        public void Construct(ScreenWrapCalculator screenWrapCalculator) {
            _screenWrapCalculator = screenWrapCalculator;
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _radius = _collider.bounds.extents.magnitude;
        }

        private void FixedUpdate() {
            _rigidbody.position = _screenWrapCalculator.GetWrappedPosition(_rigidbody.position, _radius);
        }
    }
}