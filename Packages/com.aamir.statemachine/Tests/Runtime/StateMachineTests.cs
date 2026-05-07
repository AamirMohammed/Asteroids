using System;
using NUnit.Framework;

namespace FSM.Tests {
    public class StateMachineTests {
        private class MockState : IState {
            public bool EnterCalled;
            public bool ExitCalled;
            public bool TickCalled;
            public Action OnExitCallback;

            public void OnEnter() {
                EnterCalled = true;
            }

            public void OnExit() {
                ExitCalled = true;
                OnExitCallback?.Invoke();
            }

            public void Tick() {
                TickCalled = true;
            }
        }

        private StateMachine _fsm;
        private MockState _stateA;
        private MockState _stateB;

        [SetUp]
        public void SetUp() {
            _fsm = new StateMachine();
            _stateA = new MockState();
            _stateB = new MockState();
        }

        [Test]
        public void SetState_WhenCalled_CallsOnEnter() {
            _fsm.SetState(_stateA);
            Assert.IsTrue(_stateA.EnterCalled);
        }

        [Test]
        public void SetState_WhenCalledWithSameState_DoesNotCallOnEnterAgain() {
            _fsm.SetState(_stateA);
            _stateA.EnterCalled = false;
            _fsm.SetState(_stateA);
            Assert.IsFalse(_stateA.EnterCalled);
        }

        [Test]
        public void SetState_WhenTransitioningToNewState_CallsOnExitOnPreviousState() {
            _fsm.SetState(_stateA);
            _fsm.SetState(_stateB);
            Assert.IsTrue(_stateA.ExitCalled);
        }

        [Test]
        public void SetState_WhenTransitioningToNewState_FiresStateChangedEvent() {
            IState changedTo = null;
            _fsm.StateChanged += state => changedTo = state;
            _fsm.SetState(_stateA);
            Assert.AreEqual(_stateA, changedTo);
        }

        [Test]
        public void Tick_WhenConditionIsTrue_TransitionsToNextState() {
            _fsm.SetState(_stateA);
            _fsm.AddTransition(_stateA, _stateB, () => true);
            _fsm.Tick();
            Assert.AreEqual(_stateB, _fsm.CurrentState);
        }

        [Test]
        public void Tick_WhenConditionIsFalse_StaysInCurrentState() {
            _fsm.SetState(_stateA);
            _fsm.AddTransition(_stateA, _stateB, () => false);
            _fsm.Tick();
            Assert.AreEqual(_stateA, _fsm.CurrentState);
        }

        [Test]
        public void Tick_WhenCalled_CallsTickOnCurrentState() {
            _fsm.SetState(_stateA);
            _fsm.Tick();
            Assert.IsTrue(_stateA.TickCalled);
        }

        [Test]
        public void AddAnyTransition_WhenConditionIsTrue_TransitionsFromAnyState() {
            _fsm.SetState(_stateA);
            _fsm.AddAnyTransition(_stateB, () => true);
            _fsm.Tick();
            Assert.AreEqual(_stateB, _fsm.CurrentState);
        }

        [Test]
        public void Tick_WhenNoStateSet_DoesNotThrow() {
            Assert.DoesNotThrow(() => _fsm.Tick());
        }

        [Test]
        public void AddAnyTransition_TakesPriorityOverRegularTransition() {
            MockState stateC = new MockState();
            _fsm.SetState(_stateA);
            _fsm.AddTransition(_stateA, _stateB, () => true);
            _fsm.AddAnyTransition(stateC, () => true);
            _fsm.Tick();
            Assert.AreEqual(stateC, _fsm.CurrentState);
        }

        [Test]
        public void SetState_WhenTransitioning_CallsOnExitBeforeOnEnter() {
            bool exitCalledBeforeEnter = false;
            _stateA.OnExitCallback = () => exitCalledBeforeEnter = !_stateB.EnterCalled;
            _fsm.SetState(_stateA);
            _fsm.SetState(_stateB);
            Assert.IsTrue(exitCalledBeforeEnter);
        }
    }
}