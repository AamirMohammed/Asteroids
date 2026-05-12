using Asteroids.Core;
using Asteroids.Pooling;
using Asteroids.Ship;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class GameOverStateTests {
        private IPoolRegistry _poolRegistry;
        private IShipSpawnService _shipSpawnService;
        private GameOverState _gameOverState;

        [SetUp]
        public void Setup() {
            _poolRegistry = Substitute.For<IPoolRegistry>();
            _shipSpawnService = Substitute.For<IShipSpawnService>();
            _gameOverState = new GameOverState(_poolRegistry, _shipSpawnService);
        }

        [Test]
        public void OnEnter_WhenCalled_ResetsAllPools() {
            _gameOverState.OnEnter();
            _poolRegistry.Received().ResetAllPools();
        }

        [Test]
        public void OnEnter_WhenCalled_CancelsRespawn() {
            _gameOverState.OnEnter();
            _shipSpawnService.Received().CancelRespawn();
        }
    }
}