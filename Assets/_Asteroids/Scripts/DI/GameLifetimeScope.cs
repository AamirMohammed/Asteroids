using Asteroids.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private InputReader _inputReader;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterComponent(_inputReader).As<IInputReader>();
        }
    }
}