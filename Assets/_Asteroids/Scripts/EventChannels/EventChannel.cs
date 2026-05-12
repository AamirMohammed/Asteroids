using System;

namespace Asteroids.EventChannels {
    public class EventChannel<T> : IEventChannel<T> {
        public event Action<T> Published;

        public void Publish(T data) {
            Published?.Invoke(data);
        }
    }
}