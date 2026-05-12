using Asteroids.Core;
using Asteroids.Pooling;
using NSubstitute;
using NUnit.Framework;

namespace Asteroids.Tests.EditMode {
    public class BootStateTests {
        private IPoolRegistry _poolRegistry;
        private BootState _bootState;

        [SetUp]
        public void Setup() {
            _poolRegistry = Substitute.For<IPoolRegistry>();
            _bootState = new BootState(_poolRegistry);
        }

        [Test]
        public void OnEnter_WhenCalled_InitializesPool() {
            _bootState.OnEnter();
            _poolRegistry.Received().Initialize();
        }
    }
}