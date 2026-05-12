using Asteroids.Core;
using Asteroids.Lives;
using Asteroids.Pooling;
using Asteroids.Scoring;
using Asteroids.Ship;
using Asteroids.Wave;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class GameStateServiceTests {
        private IPlayerLives _playerLives;
        private IScoreService _scoreService;
        private IShipSpawnService _shipSpawnService;
        private IWaveService _waveService;
        private IPoolRegistry _poolRegistry;
        private BootState _bootState;
        private PlayingState _playingState;
        private GameOverState _gameOverState;
        private GameStateService _gameStateService;

        [SetUp]
        public void Setup() {
            _playerLives = Substitute.For<IPlayerLives>();
            _scoreService = Substitute.For<IScoreService>();
            _shipSpawnService = Substitute.For<IShipSpawnService>();
            _waveService = Substitute.For<IWaveService>();
            _poolRegistry = Substitute.For<IPoolRegistry>();
            _bootState = new BootState(_poolRegistry);
            _playingState = new PlayingState(_playerLives, _shipSpawnService, _waveService);
            _gameOverState = new GameOverState(_poolRegistry, _shipSpawnService);
            _gameStateService = new GameStateService(
                _playerLives, _scoreService, _waveService,
                _poolRegistry, _bootState, _playingState, _gameOverState);
            _gameStateService.Initialize();
        }

        [Test]
        public void Initialize_Always_InitializesPool() {
            _poolRegistry.Received().Initialize();
        }

        [Test]
        public void Tick_WhenOutOfLives_ResetsAllPools() {
            _poolRegistry.IsReady.Returns(true);
            _gameStateService.Tick();
            _playerLives.IsOutOfLives.Returns(true);
            _gameStateService.Tick();
            _poolRegistry.Received().ResetAllPools();
        }
    }
}