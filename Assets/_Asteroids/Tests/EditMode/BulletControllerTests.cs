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
        public void Reset_AfterExpiry_IsNoLongerExpired() {
            _controller.IsExpired(2f);
            _controller.Reset();
            bool expired = _controller.IsExpired(1f);
            Assert.That(expired, Is.False);
        }

        [TestCase(0f, 1f, 0f, 10f)] // up
        [TestCase(1f, 0f, 10f, 0f)] // right
        [TestCase(0f, -1f, 0f, -10f)] // down
        [TestCase(-1f, 0f, -10f, 0f)] // left
        public void GetMovement_GivenDirection_ReturnsMovementInThatDirection(float dirX, float dirY, float expectedX,
            float expectedY) {
            Vector2 movement = _controller.GetMovement(1f, new Vector2(dirX, dirY));
            Assert.That(movement, Is.EqualTo(new Vector2(expectedX, expectedY)));
        }
    }
}