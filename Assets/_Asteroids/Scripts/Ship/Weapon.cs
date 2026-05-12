using Asteroids.Input;
using Asteroids.Pooling;
using Asteroids.Projectiles;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;

namespace Asteroids.Ship {
    public class Weapon : MonoBehaviour {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private AssetReferenceGameObject _bulletReference;

        private IPoolRegistry _poolRegistry;
        private IInputReader _inputReader;

        [Inject]
        public void Construct(IPoolRegistry poolRegistry, IInputReader inputReader) {
            _poolRegistry = poolRegistry;
            _inputReader = inputReader;
        }

        private void OnEnable() {
            _inputReader.ShootPressed += OnShootPressed;
        }

        private void OnDisable() {
            _inputReader.ShootPressed -= OnShootPressed;
        }

        private void OnShootPressed() {
            BulletComponent bulletComponent = _poolRegistry.Get<BulletComponent>(_bulletReference);
            if (bulletComponent == null) {
                return;
            }

            bulletComponent.Initialize(_firePoint.position, _firePoint.rotation);
        }
    }
}