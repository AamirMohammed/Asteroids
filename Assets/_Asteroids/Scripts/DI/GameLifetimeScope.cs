using Asteroids.Asteroid;
using Asteroids.Core;
using Asteroids.Input;
using Asteroids.Pooling;
using Asteroids.Randomization;
using Asteroids.Scoring;
using Asteroids.ScreenWrap;
using Asteroids.UI.HUD;
using Asteroids.Wave;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.DI {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PoolRegistry _poolRegistry;
        [SerializeField] private AsteroidConfig _largeAsteroidConfig;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private WaveConfig _waveConfig;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<Bootstrapper>();
            builder.RegisterEntryPoint<ScorePresenter>();
            builder.RegisterEntryPoint<ScoreSystem>().As<IScoreSystem>();
            builder.RegisterEntryPoint<AsteroidSpawnService>().As<IAsteroidSpawnService>();
            builder.RegisterEntryPoint<WaveSystem>().AsSelf();
            builder.RegisterInstance(_waveConfig).As<IWaveConfig>();
            builder.RegisterInstance(Camera.main);
            builder.RegisterInstance(_largeAsteroidConfig);
            builder.Register<CameraScreenBoundsProvider>(Lifetime.Singleton).As<IScreenBoundsProvider>();
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.Register<UnityRandomProvider>(Lifetime.Singleton).As<IRandomProvider>();
            builder.Register<AsteroidDestroyedChannel>(Lifetime.Singleton);
            builder.RegisterComponent(_inputReader).As<IInputReader>();
            builder.RegisterComponent(_poolRegistry);
            builder.RegisterComponent(_asteroidSpawner).As<IAsteroidSpawner>();
            builder.RegisterComponent(_scoreView).As<IScoreView>();
        }
    }
}