using Asteroids.Projectiles;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Tests.EditMode {
    public class BulletControllerTests {
        private IBulletConfig _config;
        private BulletController _controller;

        [SetUp]
        public void Setup() {
            _config = Substitute.For<IBulletConfig>();
            _config.Speed.Returns(10f);
            _config.Lifetime.Returns(3f);
            _controller = new BulletController(_config);
        }

        [Test]
        public void IsExpired_BeforeLifetime_ReturnsFalse() {
            bool expired = _controller.IsExpired(1f);
            Assert.That(expired, Is.False);
        }

        [Test]
        public void IsExpired_AfterLifetime_ReturnsTrue() {
            _controller.IsExpired(2f);
            bool expired = _controller.IsExpired(1.1f);
            Assert.That(expired, Is.True);
        }

        [Test]
        public void Reset_ResetsElapsedTime() {
            _controller.IsExpired(2f);
            _controller.Reset();
            bool expired = _controller.IsExpired(1f);
            Assert.That(expired, Is.False);
        }

        [Test]
        public void GetMovement_ReturnsCorrectVelocity() {
            Vector2 movement = _controller.GetMovement(1f);
            Assert.That(movement, Is.EqualTo(Vector2.up * 10f));
        }
    }
}