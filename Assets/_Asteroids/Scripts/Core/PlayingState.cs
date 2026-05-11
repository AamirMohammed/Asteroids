using Asteroids.HealthSystem;
using Asteroids.Ship;
using FSM;

namespace Asteroids.Core {
    public class PlayingState : IState {
        private readonly IHealth _health;
        private readonly IShipSpawnService _shipSpawnService;

        public PlayingState(IHealth health, IShipSpawnService shipSpawnService) {
            _health = health;
            _shipSpawnService = shipSpawnService;
        }

        public void OnEnter() {
            _health.LivesChanged += OnLivesChanged;
        }

        public void OnExit() {
            _health.LivesChanged -= OnLivesChanged;
        }

        public void Tick() {
        }

        private void OnLivesChanged(int lives) {
            _shipSpawnService.ScheduleRespawn();
        }
    }
}