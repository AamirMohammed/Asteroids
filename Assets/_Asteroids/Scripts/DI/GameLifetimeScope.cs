using Asteroids.Input;
using Asteroids.Pooling;
using Asteroids.ScreenWrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PoolRegistry _poolRegistry;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(Camera.main);
            builder.Register<CameraScreenBoundsProvider>(Lifetime.Singleton).As<IScreenBoundsProvider>();
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.RegisterComponent(_inputReader).As<IInputReader>();
            builder.RegisterComponent(_poolRegistry);
        }
    }
}