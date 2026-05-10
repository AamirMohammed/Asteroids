using System;

namespace Asteroids.EventChannels {
    public interface IEventChannel<T> {
        event Action<T> Published;
        void Publish(T data);
    }
}