using Asteroids.Pooling;
using FSM;

namespace Asteroids.Core {
    public class GameOverState : IState {
        private readonly PoolRegistry _poolRegistry;

        public GameOverState(PoolRegistry poolRegistry) {
            _poolRegistry = poolRegistry;
        }

        public void OnEnter() {
            _poolRegistry.ResetAllPools();
        }

        public void OnExit() {
        }

        public void Tick() {
        }
    }
}