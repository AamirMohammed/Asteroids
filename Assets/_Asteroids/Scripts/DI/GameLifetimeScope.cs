using Asteroids.Asteroid;
using Asteroids.Core;
using Asteroids.HealthSystem;
using Asteroids.Input;
using Asteroids.Pooling;
using Asteroids.Randomization;
using Asteroids.Scoring;
using Asteroids.ScreenWrap;
using Asteroids.Ship;
using Asteroids.UI.GameOver;
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
        [SerializeField] private WaveConfig _waveConfig;
        [SerializeField] private ShipComponent _shipComponent;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LivesView _livesView;

        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterEntryPoint<ScorePresenter>();
            builder.RegisterEntryPoint<LivesPresenter>();
            builder.RegisterEntryPoint<ScoreSystem>().As<IScoreSystem>();
            builder.RegisterEntryPoint<AsteroidSpawnService>().As<IAsteroidSpawnService>();
            builder.RegisterEntryPoint<WaveSystem>().AsSelf();
            builder.RegisterEntryPoint<GameStateService>().As<IGameStateService>();
            builder.RegisterEntryPoint<GameOverPresenter>();
            builder.RegisterInstance(_waveConfig).As<IWaveConfig>();
            builder.RegisterInstance(Camera.main);
            builder.RegisterInstance(_largeAsteroidConfig).As<IAsteroidConfig>();
            builder.RegisterInstance(_shipComponent).As<IShip>();
            builder.RegisterInstance(_playerConfig).As<IPlayerConfig>();
            builder.Register<CameraScreenBoundsProvider>(Lifetime.Singleton).As<IScreenBoundsProvider>();
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.Register<UnityRandomProvider>(Lifetime.Singleton).As<IRandomProvider>();
            builder.Register<AsteroidDestroyedChannel>(Lifetime.Singleton);
            builder.Register<Health>(Lifetime.Singleton).As<IHealth>();
            builder.Register<BootState>(Lifetime.Singleton);
            builder.Register<PlayingState>(Lifetime.Singleton);
            builder.Register<GameOverState>(Lifetime.Singleton);
            builder.RegisterComponent(_inputReader).As<IInputReader>();
            builder.RegisterComponent(_poolRegistry);
            builder.RegisterComponent(_asteroidSpawner).As<IAsteroidSpawner>();
            builder.RegisterComponent(_scoreView).As<IScoreView>();
            builder.RegisterComponent(_livesView).As<ILivesView>();
            builder.RegisterComponent(_gameOverView).As<IGameOverView>();
        }
    }
}