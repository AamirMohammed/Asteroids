using Asteroids.Lives;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;

namespace Asteroids.Core {
    public class PlayingState : IState {
        private readonly IPlayerLives _playerLives;
        private readonly IShipSpawnService _shipSpawnService;
        private readonly IWaveService _waveService;

        public PlayingState(IPlayerLives playerLives, IShipSpawnService shipSpawnService, IWaveService waveService) {
            _playerLives = playerLives;
            _shipSpawnService = shipSpawnService;
            _waveService = waveService;
        }

        public void OnEnter() {
            _shipSpawnService.Spawn();
            _waveService.StartWave();
            _playerLives.LivesChanged += OnLivesChanged;
        }

        public void OnExit() {
            _playerLives.LivesChanged -= OnLivesChanged;
        }

        public void Tick() {
        }

        private void OnLivesChanged(int lives) {
            if (lives <= 0) {
                return;
            }

            _shipSpawnService.ScheduleRespawn();
        }
    }
}