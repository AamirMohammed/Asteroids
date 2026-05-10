using Asteroids.Asteroid;
using Asteroids.Randomization;
using Asteroids.ScreenWrap;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Tests.EditMode {
    public class AsteroidSpawnControllerTests {
        private IScreenBoundsProvider _boundsProvider;
        private IRandomProvider _random;
        private AsteroidSpawnController _controller;

        [SetUp]
        public void Setup() {
            _boundsProvider = Substitute.For<IScreenBoundsProvider>();
            _boundsProvider.BottomLeft.Returns(new Vector2(-10f, -5f));
            _boundsProvider.TopRight.Returns(new Vector2(10f, 5f));

            _random = Substitute.For<IRandomProvider>();
            _controller = new AsteroidSpawnController(_boundsProvider, _random);
        }

        [Test]
        public void GetRandomEdgePosition_WhenTopEdge_ReturnsTopYPosition() {
            _random.Range(0, 4).Returns(0);
            Vector2 position = _controller.GetRandomEdgePosition();
            Assert.That(position.y, Is.EqualTo(5f));
        }

        [Test]
        public void GetRandomEdgePosition_WhenBottomEdge_ReturnsBottomYPosition() {
            _random.Range(0, 4).Returns(1);
            Vector2 position = _controller.GetRandomEdgePosition();
            Assert.That(position.y, Is.EqualTo(-5f));
        }

        [Test]
        public void GetRandomEdgePosition_WhenLeftEdge_ReturnsLeftXPosition() {
            _random.Range(0, 4).Returns(2);
            Vector2 position = _controller.GetRandomEdgePosition();
            Assert.That(position.x, Is.EqualTo(-10f));
        }

        [Test]
        public void GetRandomEdgePosition_WhenRightEdge_ReturnsRightXPosition() {
            _random.Range(0, 4).Returns(3);
            Vector2 position = _controller.GetRandomEdgePosition();
            Assert.That(position.x, Is.EqualTo(10f));
        }

        [Test]
        public void GetRandomDirection_ReturnsNormalizedVector() {
            _random.Range(0f, 360f).Returns(45f);
            Vector2 direction = _controller.GetRandomDirection();
            Assert.That(direction.magnitude, Is.EqualTo(1f).Within(0.001f));
        }

        [TestCase(1f)]
        [TestCase(2f)]
        [TestCase(3f)]
        public void GetRandomSpeed_ReturnsValueFromRandomProvider(float speed) {
            _random.Range(1f, 3f).Returns(speed);
            Assert.That(_controller.GetRandomSpeed(1f, 3f), Is.EqualTo(speed));
        }
    }
}