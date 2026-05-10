using System.Collections.Generic;

namespace Asteroids.Asteroid {
    public interface IAsteroidSpawner {
        IReadOnlyList<AsteroidPrefabEntry> PrefabEntries { get; }
    }
}