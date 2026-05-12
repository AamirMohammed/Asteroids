using Asteroids.Asteroid;
using Asteroids.Scoring;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class ScoreServiceTests {
        private ScoreService _scoreService;

        [SetUp]
        public void Setup() {
            AsteroidDestroyedChannel channel = new AsteroidDestroyedChannel();
            _scoreService = new ScoreService(channel);
            _scoreService.Initialize();
        }

        [TearDown]
        public void TearDown() {
            _scoreService.Dispose();
        }

        [Test]
        public void AddScore_WhenCalled_IncreasesScore() {
            _scoreService.AddScore(20);
            Assert.That(_scoreService.Score, Is.EqualTo(20));
        }

        [Test]
        public void AddScore_MultipleTimes_AccumulatesScore() {
            _scoreService.AddScore(20);
            _scoreService.AddScore(50);
            Assert.That(_scoreService.Score, Is.EqualTo(70));
        }

        [Test]
        public void AddScore_WhenCalled_FiresScoreChangedEvent() {
            int receivedScore = 0;
            _scoreService.ScoreChanged += score => { receivedScore = score; };
            _scoreService.AddScore(20);
            Assert.That(receivedScore, Is.EqualTo(20));
        }


        [Test]
        public void AddScore_MultipleTimes_EventFiresWithAccumulatedScore() {
            int lastReceivedScore = 0;
            _scoreService.ScoreChanged += score => { lastReceivedScore = score; };
            _scoreService.AddScore(20);
            _scoreService.AddScore(50);
            Assert.That(lastReceivedScore, Is.EqualTo(70));
        }

        [Test]
        public void Score_InitialValue_IsZero() {
            Assert.That(_scoreService.Score, Is.EqualTo(0));
        }

        [Test]
        public void Reset_WhenCalled_SetsScoreToZero() {
            _scoreService.AddScore(20);
            _scoreService.Reset();
            Assert.That(_scoreService.Score, Is.EqualTo(0));
        }

        [Test]
        public void Reset_WhenCalled_FiresScoreChangedWithZero() {
            int receivedScore = -1;
            _scoreService.AddScore(20);
            _scoreService.ScoreChanged += score => { receivedScore = score; };
            _scoreService.Reset();
            Assert.That(receivedScore, Is.EqualTo(0));
        }
    }
}