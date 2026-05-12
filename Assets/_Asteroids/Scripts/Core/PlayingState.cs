using Asteroids.Lives;
using Asteroids.Ship;
using FSM;

namespace Asteroids.Core {
    public class PlayingState : IState {
        private readonly IPlayerLives _playerLives;
        private readonly IShipSpawnService _shipSpawnService;

        public PlayingState(IPlayerLives playerLives, IShipSpawnService shipSpawnService) {
            _playerLives = playerLives;
            _shipSpawnService = shipSpawnService;
        }

        public void OnEnter() {
            _playerLives.LivesChanged += OnLivesChanged;
        }

        public void OnExit() {
            _playerLives.LivesChanged -= OnLivesChanged;
        }

        public void Tick() {
        }

        private void OnLivesChanged(int lives) {
            if(lives <= 0) return;
            _shipSpawnService.ScheduleRespawn();
        }
    }
}