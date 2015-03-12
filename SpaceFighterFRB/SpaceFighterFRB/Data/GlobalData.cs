using SpaceFighterFRB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceFighterFRB
{
    class GlobalData
    {
        public static PlayerData PlayerData
        {
            get;
            private set;
        }

        public static EnemyData EnemyData
        {
            get;
            private set;
        }

        public static void Initialize()
        {
            PlayerData = new PlayerData();
            EnemyData = new EnemyData();
        }
    }
}
