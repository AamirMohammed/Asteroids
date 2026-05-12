using Asteroids.Core;
using Asteroids.Ship;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Tests.EditMode {
    public class ShipSpawnServiceTests {
        private IShip _ship;
        private IPlayerConfig _playerConfig;
        private ShipSpawnService _shipSpawnService;

        [SetUp]
        public void Setup() {
            _ship = Substitute.For<IShip>();
            _playerConfig = Substitute.For<IPlayerConfig>();
            _playerConfig.RespawnDelay.Returns(2f);
            _shipSpawnService = new ShipSpawnService(_ship, _playerConfig);
        }

        [Test]
        public void Spawn_Always_TeleportsShipToZero() {
            _shipSpawnService.Spawn();
            _ship.Received().Teleport(Vector3.zero);
        }

        [Test]
        public void Spawn_Always_ShowsShip() {
            _shipSpawnService.Spawn();
            _ship.Received().Show();
        }

        [Test]
        public void ScheduleRespawn_Always_HidesShip() {
            _shipSpawnService.ScheduleRespawn();
            _ship.Received().Hide();
        }

        [Test]
        public void CancelRespawn_AfterSchedule_DoesNotSpawnOnTick() {
            _shipSpawnService.ScheduleRespawn();
            _shipSpawnService.CancelRespawn();
            _shipSpawnService.Tick();
            _ship.DidNotReceive().Show();
        }
    }
}