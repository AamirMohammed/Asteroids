using Asteroids.Pooling;
using Asteroids.Ship;
using FSM;

namespace Asteroids.Core {
    public class GameOverState : IState {
        private readonly PoolRegistry _poolRegistry;
        private readonly IShipSpawnService _shipSpawnService;

        public GameOverState(PoolRegistry poolRegistry, IShipSpawnService shipSpawnService) {
            _poolRegistry = poolRegistry;
            _shipSpawnService = shipSpawnService;
        }

        public void OnEnter() {
            _poolRegistry.ResetAllPools();
            _shipSpawnService.CancelRespawn();
        }

        public void OnExit() {
        }

        public void Tick() {
        }
    }
}