using System;
using System.Collections;
using Asteroids.Input;
using Asteroids.Ship;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace Asteroids.Tests.PlayMode {
    public class ShipMovementTests {
        private GameObject _ship;
        private ShipMovement _shipMovement;
        private IInputReader _inputReader;

        [SetUp]
        public void Setup() {
            _ship = new GameObject();
            _ship.SetActive(false);
            _ship.AddComponent<Rigidbody2D>().gravityScale = 0;
            _shipMovement = _ship.AddComponent<ShipMovement>();
            _inputReader = Substitute.For<IInputReader>();
            _shipMovement.Construct(_inputReader);
            _ship.SetActive(true);
        }

        [TearDown]
        public void TearDown() {
            Object.Destroy(_ship);
        }

        [UnityTest]
        public IEnumerator ShipMovement_WhenThrusting_MovesForward() {
            Vector3 initialPosition = _ship.transform.position;
            _inputReader.ThrustInput += Raise.Event<Action<bool>>(true);
            yield return new WaitForFixedUpdate();
            Assert.That(_ship.transform.position.y, Is.GreaterThan(initialPosition.y));
        }

        [UnityTest]
        public IEnumerator ShipMovement_WhenNotThrusting_DoesNotMove() {
            Vector3 initialPosition = _ship.transform.position;
            _inputReader.ThrustInput += Raise.Event<Action<bool>>(false);
            yield return new WaitForFixedUpdate();
            Assert.That(_ship.transform.position, Is.EqualTo(initialPosition));
        }

        [UnityTest]
        public IEnumerator ShipMovement_WhenRotatingRight_RotatesClockwise() {
            Rigidbody2D rb = _ship.GetComponent<Rigidbody2D>();
            float initialRotation = rb.rotation;
            _inputReader.RotateInput += Raise.Event<Action<float>>(1f);
            yield return new WaitForFixedUpdate();
            Assert.That(rb.rotation, Is.LessThan(initialRotation));
        }

        [UnityTest]
        public IEnumerator ShipMovement_WhenRotatingLeft_RotatesCounterClockwise() {
            Rigidbody2D rb = _ship.GetComponent<Rigidbody2D>();
            float initialRotation = rb.rotation;
            _inputReader.RotateInput += Raise.Event<Action<float>>(-1f);
            yield return new WaitForFixedUpdate();
            Assert.That(rb.rotation, Is.GreaterThan(initialRotation));
        }
    }
}