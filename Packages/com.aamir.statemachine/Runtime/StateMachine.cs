using System;
using System.Collections.Generic;

namespace FSM {
    public class StateMachine {
        private List<StateTransition> _stateTransitions = new List<StateTransition>();
        private List<StateTransition> _anyStateTransition = new List<StateTransition>();

        public IState CurrentState { get; private set; }
        public event Action<IState> StateChanged;

        public void AddTransition(IState from, IState to, Func<bool> condition) {
            StateTransition stateTransition = new StateTransition(from, to, condition);
            _stateTransitions.Add(stateTransition);
        }

        public void AddAnyTransition(IState to, Func<bool> condition) {
            StateTransition stateTransition = new StateTransition(null, to, condition);
            _anyStateTransition.Add(stateTransition);
        }

        public void SetState(IState state) {
            if (CurrentState == state) {
                return;
            }

            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();

            StateChanged?.Invoke(CurrentState);
        }

        public void Tick() {
            StateTransition transition = CheckForTransition();
            if (transition != null) {
                SetState(transition.To);
            }

            CurrentState?.Tick();
        }

        private StateTransition CheckForTransition() {
            foreach (StateTransition stateTransition in _anyStateTransition) {
                if (stateTransition.Condition()) {
                    return stateTransition;
                }
            }

            foreach (StateTransition stateTransition in _stateTransitions) {
                if (stateTransition.From == CurrentState && stateTransition.Condition()) {
                    return stateTransition;
                }
            }

            return null;
        }
    }
}