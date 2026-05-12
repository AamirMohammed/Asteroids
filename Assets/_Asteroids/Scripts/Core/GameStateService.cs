using Asteroids.Lives;
using Asteroids.Pooling;
using Asteroids.Scoring;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;
using VContainer.Unity;

namespace Asteroids.Core {
    public class GameStateService : IInitializable, ITickable, IGameStateService {
        private readonly StateMachine _stateMachine;
        private readonly IPlayerLives _playerLives;
        private readonly IScoreSystem _scoreSystem;
        private readonly IShipSpawnService _shipSpawnService;
        private readonly IWaveSystem _waveSystem;
        private readonly PoolRegistry _poolRegistry;

        private readonly BootState _bootState;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;

        public GameStateService(
            IPlayerLives playerLives, IScoreSystem scoreSystem,
            IShipSpawnService shipSpawnService, IWaveSystem waveSystem,
            PoolRegistry poolRegistry, BootState bootState,
            PlayingState playingState, GameOverState gameOverState) {
            _playerLives = playerLives;
            _scoreSystem = scoreSystem;
            _shipSpawnService = shipSpawnService;
            _waveSystem = waveSystem;
            _poolRegistry = poolRegistry;
            _bootState = bootState;
            _playingState = playingState;
            _gameOverState = gameOverState;
            _stateMachine = new StateMachine();
        }

        public void Initialize() {
            _stateMachine.AddTransition(_bootState, _playingState, () => _poolRegistry.IsReady);
            _stateMachine.AddTransition(_playingState, _gameOverState, () => _playerLives.IsDead);
            _stateMachine.SetState(_bootState);
        }

        public void Tick() {
            _stateMachine.Tick();
        }

        public void Restart() {
            _playerLives.Reset();
            _scoreSystem.Reset();
            _waveSystem.Reset();
            _shipSpawnService.Spawn();
            _waveSystem.StartWave();
            _stateMachine.SetState(_playingState);
        }
    }
}