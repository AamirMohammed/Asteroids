using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Pooling {
    public class PoolItem : MonoBehaviour {
        public ObjectPool<PoolItem> Pool { get; set; }
    }
}