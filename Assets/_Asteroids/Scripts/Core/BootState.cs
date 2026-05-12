using Asteroids.Pooling;
using FSM;

namespace Asteroids.Core {
    public class BootState : IState {
        private readonly IPoolRegistry _poolRegistry;

        public BootState(IPoolRegistry poolRegistry) {
            _poolRegistry = poolRegistry;
        }

        public void OnEnter() {
            _poolRegistry.Initialize();
        }

        public void OnExit() {
        }

        public void Tick() {
        }
    }
}