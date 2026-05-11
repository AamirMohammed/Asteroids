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
        [Header("Components")]
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private PoolRegistry _poolRegistry;
        [SerializeField] private ShipComponent _shipComponent;

        [Header("Configs")]
        [SerializeField] private ShipConfig _shipConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private AsteroidConfig _largeAsteroidConfig;
        [SerializeField] private WaveConfig _waveConfig;

        [Header("Views")]
        [SerializeField] private GameOverView _gameOverView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private LivesView _livesView;

        protected override void Configure(IContainerBuilder builder) {
            // Entry Points
            builder.RegisterEntryPoint<GameStateService>().As<IGameStateService>();
            builder.RegisterEntryPoint<ShipSpawnService>().As<IShipSpawnService>();
            builder.RegisterEntryPoint<WaveSystem>().As<IWaveSystem>();
            builder.RegisterEntryPoint<AsteroidSpawnService>().As<IAsteroidSpawnService>();
            builder.RegisterEntryPoint<ScoreSystem>().As<IScoreSystem>();
            builder.RegisterEntryPoint<ScorePresenter>();
            builder.RegisterEntryPoint<LivesPresenter>();
            builder.RegisterEntryPoint<GameOverPresenter>();

            // Configs
            builder.RegisterInstance(_shipConfig).As<IShipConfig>();
            builder.RegisterInstance(_playerConfig).As<IPlayerConfig>();
            builder.RegisterInstance(_waveConfig).As<IWaveConfig>();
            builder.RegisterInstance(_largeAsteroidConfig).As<IAsteroidConfig>();

            // Services
            builder.Register<Health>(Lifetime.Singleton).As<IHealth>();
            builder.Register<AsteroidDestroyedChannel>(Lifetime.Singleton);
            builder.Register<AsteroidSpawnController>(Lifetime.Singleton);
            builder.Register<CameraScreenBoundsProvider>(Lifetime.Singleton).As<IScreenBoundsProvider>();
            builder.Register<ScreenWrapCalculator>(Lifetime.Singleton);
            builder.Register<UnityRandomProvider>(Lifetime.Singleton).As<IRandomProvider>();

            // States
            builder.Register<BootState>(Lifetime.Singleton);
            builder.Register<PlayingState>(Lifetime.Singleton);
            builder.Register<GameOverState>(Lifetime.Singleton);

            // Components
            builder.RegisterComponent(_shipComponent).As<IShip>();
            builder.RegisterComponent(_inputReader).As<IInputReader>();
            builder.RegisterComponent(_poolRegistry);
            builder.RegisterInstance(Camera.main);

            // UI
            builder.RegisterComponent(_scoreView).As<IScoreView>();
            builder.RegisterComponent(_livesView).As<ILivesView>();
            builder.RegisterComponent(_gameOverView).As<IGameOverView>();
        }
    }
}