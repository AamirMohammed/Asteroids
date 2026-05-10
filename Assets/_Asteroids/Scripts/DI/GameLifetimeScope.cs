using Asteroids.Asteroid;
using Asteroids.Core;
using Asteroids.Input;
using Asteroids.Pooling;
using Asteroids.Randomization;
using Asteroids.Scoring;
using Asteroids.ScreenWrap;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PoolRegistry _poolRegistry;
        [SerializeField] private AsteroidConfig _asteroidConfig;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<Bootstrapper>();
            builder.RegisterInstance(Camera.main);
            builder.RegisterInstance(_asteroidConfig).As<IAsteroidConfig>();
            builder.Register<CameraScreenBoundsProvider>(Lifetime.Singleton).As<IScreenBoundsProvider>();
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.Register<AsteroidSpawner>(Lifetime.Singleton);
            builder.Register<UnityRandomProvider>(Lifetime.Singleton).As<IRandomProvider>();
            builder.Register<ScoreSystem>(Lifetime.Singleton).As<IScoreSystem>();
            builder.RegisterComponent(_inputReader).As<IInputReader>();
            builder.RegisterComponent(_poolRegistry);
        }
    }
}