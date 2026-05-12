namespace Asteroids.Ship {
    public interface IShipSpawnService {
        void Spawn();
        void ScheduleRespawn();
        void CancelRespawn();
    }
}