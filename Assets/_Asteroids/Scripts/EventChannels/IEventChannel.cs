namespace Asteroids.EventChannels {
    public interface IEventChannel<T> : IReadOnlyEventChannel<T> {
        void Publish(T data);
    }
}