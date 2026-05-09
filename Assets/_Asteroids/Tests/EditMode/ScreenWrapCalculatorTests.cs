using Asteroids.ScreenWrap;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Asteroids.Tests.EditMode {
    public class ScreenWrapCalculatorTests {
        private IScreenBoundsProvider _boundsProvider;
        private ScreenWrapCalculator _calculator;

        [SetUp]
        public void Setup() {
            _boundsProvider = Substitute.For<IScreenBoundsProvider>();
            _boundsProvider.BottomLeft.Returns(new Vector2(-10f, -5f));
            _boundsProvider.TopRight.Returns(new Vector2(10f, 5f));
            _calculator = new ScreenWrapCalculator(_boundsProvider);
        }

        [Test]
        public void GetWrappedPosition_WhenExitedRight_AppearsOutsideLeft() {
            Vector2 position = new Vector2(11f, 0f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result.x, Is.EqualTo(-10.5f));
        }

        [Test]
        public void GetWrappedPosition_WhenExitedLeft_AppearsOutsideRight() {
            Vector2 position = new Vector2(-11f, 0f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result.x, Is.EqualTo(10.5f));
        }

        [Test]
        public void GetWrappedPosition_WhenExitedTop_AppearsOutsideBottom() {
            Vector2 position = new Vector2(0f, 6f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result.y, Is.EqualTo(-5.5f));
        }

        [Test]
        public void GetWrappedPosition_WhenExitedBottom_AppearsOutsideTop() {
            Vector2 position = new Vector2(0f, -6f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result.y, Is.EqualTo(5.5f));
        }

        [Test]
        public void GetWrappedPosition_WhenPartiallyOffRight_DoesNotWrap() {
            Vector2 position = new Vector2(10f, 0f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result.x, Is.EqualTo(10f));
        }

        [Test]
        public void GetWrappedPosition_WhenInsideScreen_DoesNotWrap() {
            Vector2 position = new Vector2(0f, 0f);
            Vector2 result = _calculator.GetWrappedPosition(position, 0.5f);
            Assert.That(result, Is.EqualTo(position));
        }
    }
}