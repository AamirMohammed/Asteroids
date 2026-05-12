using System;

namespace Asteroids.EventChannels {
    public interface IReadOnlyEventChannel<T> {
        event Action<T> Published;
    }
}