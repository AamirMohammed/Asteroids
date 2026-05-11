using Asteroids.HealthSystem;
using Asteroids.Pooling;
using Asteroids.Scoring;
using Asteroids.Ship;
using Asteroids.Wave;
using FSM;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Core {
    public class GameStateService : IInitializable, ITickable, IGameStateService {
        private readonly StateMachine _stateMachine;
        private readonly IHealth _health;
        private readonly IScoreSystem _scoreSystem;
        private readonly ShipMovement _shipMovement;
        private readonly WaveSystem _waveSystem;
        private readonly PoolRegistry _poolRegistry;

        private readonly BootState _bootState;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;

        public GameStateService(
            IHealth health, IScoreSystem scoreSystem,
            ShipMovement shipMovement, WaveSystem waveSystem,
            PoolRegistry poolRegistry,
            BootState bootState,
            PlayingState playingState,
            GameOverState gameOverState) {
            _health = health;
            _scoreSystem = scoreSystem;
            _shipMovement = shipMovement;
            _waveSystem = waveSystem;
            _poolRegistry = poolRegistry;
            _bootState = bootState;
            _playingState = playingState;
            _gameOverState = gameOverState;
            _stateMachine = new StateMachine();
        }

        public void Initialize() {
            _stateMachine.AddTransition(_bootState, _playingState, () => _poolRegistry.IsReady);
            _stateMachine.AddTransition(_playingState, _gameOverState, () => _health.IsDead);
            _stateMachine.SetState(_bootState);
        }

        public void Tick() {
            _stateMachine.Tick();
        }

        public void Restart() {
            _health.Reset();
            _scoreSystem.Reset();
            _waveSystem.Reset();
            _shipMovement.gameObject.SetActive(true);
            _shipMovement.Teleport(Vector3.zero);
            _waveSystem.StartWave();
            _stateMachine.SetState(_playingState);
        }
    }
}