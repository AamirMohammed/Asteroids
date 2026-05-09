using UnityEngine;

namespace Asteroids.ScreenWrap {
    public interface IScreenBoundsProvider {
        Vector2 BottomLeft { get; }
        Vector2 TopRight { get; }
    }
}