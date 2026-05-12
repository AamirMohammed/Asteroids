using Asteroids.Asteroid;
using Asteroids.Wave;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class WaveSystemTests {
        private AsteroidDestroyedChannel _channel;
        private IAsteroidSpawnService _spawnService;
        private IWaveConfig _waveConfig;
        private IAsteroidConfig _largeConfig;
        private WaveSystem _waveSystem;

        [SetUp]
        public void Setup() {
            _channel = new AsteroidDestroyedChannel();
            _spawnService = Substitute.For<IAsteroidSpawnService>();
            _waveConfig = Substitute.For<IWaveConfig>();
            _largeConfig = Substitute.For<IAsteroidConfig>();

            _waveConfig.GetAsteroidCount(Arg.Any<int>()).Returns(4);
            _largeConfig.SplitCount.Returns(0);
            _largeConfig.CanSplit.Returns(false);
            _largeConfig.SplitIntoConfig.Returns((IAsteroidConfig)null);

            _waveSystem = new WaveSystem(_spawnService, _channel, _largeConfig, _waveConfig);
            _waveSystem.Initialize();
        }

        [TearDown]
        public void TearDown() {
            _waveSystem.Dispose();
        }

        [Test]
        public void StartWave_WhenCalled_CallsSpawnWave() {
            _waveSystem.StartWave();
            _spawnService.Received().SpawnWave(4, _largeConfig);
        }

        [Test]
        public void StartWave_WhenCalledTwice_IncrementsWaveCount() {
            _waveSystem.StartWave();
            _waveSystem.StartWave();
            _waveConfig.Received().GetAsteroidCount(2);
        }

        [Test]
        public void OnAsteroidDestroyed_WhenRemainingReachesZero_StartsNextWave() {
            _waveSystem.StartWave();
            _spawnService.ClearReceivedCalls();

            for (int i = 0; i < 4; i++) {
                _channel.Publish(new AsteroidDestroyedData(null, default));
            }

            _spawnService.Received().SpawnWave(Arg.Any<int>(), Arg.Any<IAsteroidConfig>());
        }

        [Test]
        public void Reset_WhenCalled_SetsWaveToZero() {
            _waveSystem.StartWave();
            _waveSystem.StartWave();
            _waveSystem.Reset();
            _waveSystem.StartWave();
            _waveConfig.Received().GetAsteroidCount(1);
        }
    }
}