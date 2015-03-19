using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceFighterFRB.Data
{
    class EnemyData
    {
        public int enemyType { get; set; }
        public int enemiesKilled { get; set; }
        public int enemiesSpawned { get; set; }
        public int waveCounter { get; set; }
        public double spawnTime { get; set; }
    }
}
