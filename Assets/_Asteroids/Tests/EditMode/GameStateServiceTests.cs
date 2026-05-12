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
        public void Tick_WhenPoolIsReady_TransitionsToPlayingState() {
            _poolRegistry.IsReady.Returns(true);
            _gameStateService.Tick();
            _shipSpawnService.Received().Spawn();
        }

        [Test]
        public void Tick_WhenOutOfLives_TransitionsToGameOverState() {
            _poolRegistry.IsReady.Returns(true);
            _gameStateService.Tick();
            _playerLives.IsOutOfLives.Returns(true);
            _gameStateService.Tick();
            _poolRegistry.Received().ResetAllPools();
        }

        [Test]
        public void Restart_WhenCalled_ResetsLives() {
            _gameStateService.Restart();
            _playerLives.Received().Reset();
        }

        [Test]
        public void Restart_WhenCalled_ResetsScore() {
            _gameStateService.Restart();
            _scoreService.Received().Reset();
        }

        [Test]
        public void Restart_WhenCalled_ResetsWave() {
            _gameStateService.Restart();
            _waveService.Received().Reset();
        }
    }
    
    
}