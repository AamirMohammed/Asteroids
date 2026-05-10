using System;
using Asteroids.HealthSystem;
using Asteroids.Pooling;
using FSM;
using VContainer.Unity;

namespace Asteroids.Core {
    public class GameStateService : IInitializable, IDisposable, ITickable {
        private readonly StateMachine _stateMachine;
        private readonly IHealth _health;
        private readonly PoolRegistry _poolRegistry;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;

        public GameStateService(IHealth health, PoolRegistry poolRegistry,
            PlayingState playingState, GameOverState gameOverState) {
            _health = health;
            _poolRegistry = poolRegistry;
            _playingState = playingState;
            _gameOverState = gameOverState;
            _stateMachine = new StateMachine();
        }

        public void Initialize() {
            _stateMachine.AddTransition(_playingState, _gameOverState, () => _health.IsDead);
            _health.Died += OnPlayerDied;
            _poolRegistry.OnReady += OnPoolsReady;
        }

        public void Dispose() {
            _health.Died -= OnPlayerDied;
            _poolRegistry.OnReady -= OnPoolsReady;
        }

        public void Tick() {
            _stateMachine.Tick();
        }

        private void OnPoolsReady() {
            _poolRegistry.OnReady -= OnPoolsReady;
            _stateMachine.SetState(_playingState);
        }

        private void OnPlayerDied() {
            _stateMachine.SetState(_gameOverState);
        }
    }
}