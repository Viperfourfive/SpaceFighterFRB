using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vector3 = Microsoft.Xna.Framework.Vector3;

namespace SpaceFighterFRB.Data
{
    class PlayerData
    {
        public int score { get; set; }
        public Vector3 position { get; set; }
        public float rotation { get; set; }
    }
}
