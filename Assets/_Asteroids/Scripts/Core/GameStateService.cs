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
        private readonly IScoreService _scoreService;
        private readonly IWaveService _waveService;
        private readonly IPoolRegistry _poolRegistry;

        private readonly BootState _bootState;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;

        public GameStateService(
            IPlayerLives playerLives, IScoreService scoreService,
            IWaveService waveService, IPoolRegistry poolRegistry, 
            BootState bootState, PlayingState playingState, 
            GameOverState gameOverState) {
            _playerLives = playerLives;
            _scoreService = scoreService;
            _waveService = waveService;
            _poolRegistry = poolRegistry;
            _bootState = bootState;
            _playingState = playingState;
            _gameOverState = gameOverState;
            _stateMachine = new StateMachine();
        }

        public void Initialize() {
            _stateMachine.AddTransition(_bootState, _playingState, () => _poolRegistry.IsReady);
            _stateMachine.AddTransition(_playingState, _gameOverState, () => _playerLives.IsOutOfLives);
            _stateMachine.SetState(_bootState);
        }

        public void Tick() {
            _stateMachine.Tick();
        }

        public void Restart() {
            _playerLives.Reset();
            _scoreService.Reset();
            _waveService.Reset();
            _stateMachine.SetState(_playingState);
        }
    }
}