using Asteroids.Core;
using Asteroids.Lives;
using Asteroids.Ship;
using Asteroids.Wave;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class PlayingStateTests {
        private IPlayerLives _playerLives;
        private IShipSpawnService _shipSpawnService;
        private IWaveService _waveService;
        private PlayingState _playingState;

        [SetUp]
        public void Setup() {
            _playerLives = Substitute.For<IPlayerLives>();
            _shipSpawnService = Substitute.For<IShipSpawnService>();
            _waveService = Substitute.For<IWaveService>();
            _playingState = new PlayingState(_playerLives, _shipSpawnService, _waveService);
        }

        [Test]
        public void OnEnter_WhenCalled_SpawnsShip() {
            _playingState.OnEnter();
            _shipSpawnService.Received().Spawn();
        }

        [Test]
        public void OnEnter_WhenCalled_StartsWave() {
            _playingState.OnEnter();
            _waveService.Received().StartWave();
        }

        [Test]
        public void OnLivesChanged_WhenLivesAboveZero_SchedulesRespawn() {
            _playingState.OnEnter();
            _playerLives.LivesChanged += Raise.Event<System.Action<int>>(2);
            _shipSpawnService.Received().ScheduleRespawn();
        }

        [Test]
        public void OnLivesChanged_WhenLivesReachZero_DoesNotScheduleRespawn() {
            _playingState.OnEnter();
            _playerLives.LivesChanged += Raise.Event<System.Action<int>>(0);
            _shipSpawnService.DidNotReceive().ScheduleRespawn();
        }
    }
}