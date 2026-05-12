using Asteroids.Core;
using Asteroids.Lives;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class PlayerLivesTests {
        private IPlayerConfig _config;
        private PlayerLives _playerLives;

        [SetUp]
        public void Setup() {
            _config = Substitute.For<IPlayerConfig>();
            _config.Lives.Returns(3);
            _playerLives = new PlayerLives(_config);
        }

        [Test]
        public void LoseLife_WhenLivesRemaining_DecreasesLivesByOne() {
            _playerLives.LoseLife();
            Assert.That(_playerLives.Lives, Is.EqualTo(2));
        }

        [Test]
        public void LoseLife_WhenOutOfLives_DoesNotDecreaseLives() {
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            Assert.That(_playerLives.Lives, Is.EqualTo(0));
        }

        [Test]
        public void LoseLife_WhenLivesRemaining_InvokesLivesChanged() {
            bool eventFired = false;
            _playerLives.LivesChanged += _ => { eventFired = true; };
            _playerLives.LoseLife();
            Assert.That(eventFired, Is.True);
        }

        [Test]
        public void LoseLife_WhenLastLife_InvokesLivesDepleted() {
            bool eventFired = false;
            _playerLives.LivesDepleted += () => { eventFired = true; };
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            Assert.That(eventFired, Is.True);
        }

        [Test]
        public void LoseLife_WhenNotLastLife_DoesNotInvokeLivesDepleted() {
            bool eventFired = false;
            _playerLives.LivesDepleted += () => eventFired = true;
            _playerLives.LoseLife();
            Assert.That(eventFired, Is.False);
        }

        [Test]
        public void Reset_AfterLosingLife_RestoresLivesToConfig() {
            _playerLives.LoseLife();
            _playerLives.Reset();
            Assert.That(_playerLives.Lives, Is.EqualTo(3));
        }

        [Test]
        public void Reset_Always_InvokesLivesChanged() {
            bool eventFired = false;
            _playerLives.LivesChanged += _ => eventFired = true;
            _playerLives.Reset();
            Assert.That(eventFired, Is.True);
        }

        [Test]
        public void IsOutOfLives_WhenLivesRemaining_ReturnsFalse() {
            Assert.That(_playerLives.IsOutOfLives, Is.False);
        }

        [Test]
        public void IsOutOfLives_WhenNoLivesRemaining_ReturnsTrue() {
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            _playerLives.LoseLife();
            Assert.That(_playerLives.IsOutOfLives, Is.True);
        }
    }
}