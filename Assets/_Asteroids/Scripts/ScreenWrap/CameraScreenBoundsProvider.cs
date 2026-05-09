using UnityEngine;

namespace Asteroids.ScreenWrap {
    public class CameraScreenBoundsProvider : IScreenBoundsProvider {
        private readonly Camera _camera;

        public CameraScreenBoundsProvider(Camera camera) {
            _camera = camera;
        }

        public Vector2 BottomLeft => _camera.ViewportToWorldPoint(Vector2.zero);
        public Vector2 TopRight => _camera.ViewportToWorldPoint(Vector2.one);
    }
}