using Asteroids.Asteroid;
using Asteroids.Scoring;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class ScoreSystemTests {
        private ScoreSystem _scoreSystem;

        [SetUp]
        public void Setup() {
            AsteroidDestroyedChannel channel = new AsteroidDestroyedChannel();
            _scoreSystem = new ScoreSystem(channel);
            _scoreSystem.Initialize();
        }

        [TearDown]
        public void TearDown() {
            _scoreSystem.Dispose();
        }

        [Test]
        public void AddScore_WhenCalled_IncreasesScore() {
            _scoreSystem.AddScore(20);
            Assert.That(_scoreSystem.Score, Is.EqualTo(20));
        }

        [Test]
        public void AddScore_MultipleTimes_AccumulatesScore() {
            _scoreSystem.AddScore(20);
            _scoreSystem.AddScore(50);
            Assert.That(_scoreSystem.Score, Is.EqualTo(70));
        }

        [Test]
        public void AddScore_WhenCalled_FiresScoreChangedEvent() {
            int receivedScore = 0;
            _scoreSystem.ScoreChanged += score => { receivedScore = score; };
            _scoreSystem.AddScore(20);
            Assert.That(receivedScore, Is.EqualTo(20));
        }


        [Test]
        public void AddScore_MultipleTimes_EventFiresWithAccumulatedScore() {
            int lastReceivedScore = 0;
            _scoreSystem.ScoreChanged += score => { lastReceivedScore = score; };
            _scoreSystem.AddScore(20);
            _scoreSystem.AddScore(50);
            Assert.That(lastReceivedScore, Is.EqualTo(70));
        }

        [Test]
        public void Score_InitialValue_IsZero() {
            Assert.That(_scoreSystem.Score, Is.EqualTo(0));
        }

        [Test]
        public void Reset_WhenCalled_SetsScoreToZero() {
            _scoreSystem.AddScore(20);
            _scoreSystem.Reset();
            Assert.That(_scoreSystem.Score, Is.EqualTo(0));
        }

        [Test]
        public void Reset_WhenCalled_FiresScoreChangedWithZero() {
            int receivedScore = -1;
            _scoreSystem.AddScore(20);
            _scoreSystem.ScoreChanged += score => { receivedScore = score; };
            _scoreSystem.Reset();
            Assert.That(receivedScore, Is.EqualTo(0));
        }
    }
}