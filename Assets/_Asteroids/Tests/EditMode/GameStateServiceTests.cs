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
        private IScoreSystem _scoreSystem;
        private IShipSpawnService _shipSpawnService;
        private IWaveSystem _waveSystem;
        private IPoolRegistry _poolRegistry;
        private BootState _bootState;
        private PlayingState _playingState;
        private GameOverState _gameOverState;
        private GameStateService _gameStateService;

        [SetUp]
        public void Setup() {
            _playerLives = Substitute.For<IPlayerLives>();
            _scoreSystem = Substitute.For<IScoreSystem>();
            _shipSpawnService = Substitute.For<IShipSpawnService>();
            _waveSystem = Substitute.For<IWaveSystem>();
            _poolRegistry = Substitute.For<IPoolRegistry>();
            _bootState = new BootState(_poolRegistry, _shipSpawnService, _waveSystem);
            _playingState = new PlayingState(_playerLives, _shipSpawnService);
            _gameOverState = new GameOverState(_poolRegistry, _shipSpawnService);
            _gameStateService = new GameStateService(
                _playerLives, _scoreSystem, _shipSpawnService, _waveSystem,
                _poolRegistry, _bootState, _playingState, _gameOverState);
            _gameStateService.Initialize();
        }

        [Test]
        public void Initialize_Always_InitializesPool() {
            _poolRegistry.Received().Initialize();
        }

        [Test]
        public void Tick_WhenPoolsReady_SpawnsShipAndStartsWave() {
            _poolRegistry.IsReady.Returns(true);
            _gameStateService.Tick();
            _shipSpawnService.Received().Spawn();
            _waveSystem.Received().StartWave();
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