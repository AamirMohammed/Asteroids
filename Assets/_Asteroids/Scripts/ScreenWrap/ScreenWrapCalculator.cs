using UnityEngine;

namespace Asteroids.ScreenWrap {
    public class ScreenWrapCalculator {
        private readonly IScreenBoundsProvider _screenBoundsProvider;

        public ScreenWrapCalculator(IScreenBoundsProvider screenBoundsProvider) {
            _screenBoundsProvider = screenBoundsProvider;
        }

        public Vector2 GetWrappedPosition(Vector2 position, float radius) {
            Vector2 bottomLeft = _screenBoundsProvider.BottomLeft;
            Vector2 topRight = _screenBoundsProvider.TopRight;

            float x = position.x;
            float y = position.y;

            bool exitedRight = x - radius > topRight.x;
            bool exitedLeft = x + radius < bottomLeft.x;
            bool exitedTop = y - radius > topRight.y;
            bool exitedBottom = y + radius < bottomLeft.y;

            if (exitedRight) {
                x = bottomLeft.x - radius;
            }
            else if (exitedLeft) {
                x = topRight.x + radius;
            }

            if (exitedTop) {
                y = bottomLeft.y - radius;
            }
            else if (exitedBottom) {
                y = topRight.y + radius;
            }

            return new Vector2(x, y);
        }
    }
}