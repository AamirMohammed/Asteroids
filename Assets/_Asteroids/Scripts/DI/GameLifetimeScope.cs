using Asteroids.Input;
using Asteroids.ScreenWrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private InputReader _inputReader;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(Camera.main);
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.RegisterComponent(_inputReader).As<IInputReader>();
        }
    }
}