#region Usings

using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.Instructions;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using SpaceFighterFRB.Factories;

#endif
#endregion

namespace SpaceFighterFRB.Entities
{
	public partial class enemySpawner
	{
        double mLastSpawnTime;
        bool isTimeToSpawn
        {
            get
            {
                float spawnFrequency = 1 / enemiesPerSecond;
                return FlatRedBall.Screens.ScreenManager.CurrentScreen.PauseAdjustedSecondsSince(mLastSpawnTime) > spawnFrequency;
            }
        }
        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{


		}

		private void CustomActivity()
		{
            if (isTimeToSpawn)
            {
                SpawnEnemy();
            }

            //increases enemy spawn speed, statically, by spawnRateIncrease
            //this.enemiesPerSecond += TimeManager.SecondDifference * this.spawnRateIncrease;
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        void SpawnEnemy()
        {
            Vector3 position = GetNewEnemyPosition();
            Vector3 angle = FindAngleToCenterScreen(position);

            enemyShip _es = enemyShipFactory.CreateNew();
            _es.Position = position;
            _es.Velocity = angle * _es.movementSpeed; //need to reference enemyShip?

            mLastSpawnTime = FlatRedBall.Screens.ScreenManager.CurrentScreen.PauseAdjustedCurrentTime;
        }

        Vector3 GetNewEnemyPosition()
        {
            int randomCorner = FlatRedBallServices.Random.Next(4);

            //Creates at screen edges, change to this after testing.
            //float top = SpriteManager.Camera.AbsoluteTopYEdgeAt(0);
            //float bottom = SpriteManager.Camera.AbsoluteBottomYEdgeAt(0);
            //float left = SpriteManager.Camera.AbsoluteLeftXEdgeAt(0);
            //float right = SpriteManager.Camera.AbsoluteRightXEdgeAt(0);
            float top = 275;
            float bottom = -275;
            float left = -375;
            float right = 375;

            int x = 0;
            int y = 0;
            switch (randomCorner)
            {
                case 0: //topLeft
                    x = (int)left;
                    y = (int)top;
                    break;
                case 1: //topRight
                    x = (int)right;
                    y = (int)top;
                    break;
                case 2: //bottomLEft
                    x = (int)left;
                    y = (int)bottom;
                    break;
                case 3: //bottomRight
                    x = (int)right;
                    y = (int)bottom;
                    break;
            }
            return new Vector3(x, y, 0);
        }

         private Vector3 FindAngleToCenterScreen(Vector3 position)
         {
             Vector3 angleToPlayer = position;
             angleToPlayer.Normalize();
         
             return angleToPlayer;
         }
	}
}
