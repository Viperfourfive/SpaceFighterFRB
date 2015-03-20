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

using FlatRedBall.Graphics.Model;
using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FlatRedBall.Localization;

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using SpaceFighterFRB.Entities;
using Microsoft.Xna.Framework.Input;
#endif
#endregion

namespace SpaceFighterFRB.Screens
{
	public partial class gameScreen
	{
		void CustomInitialize()
		{
            CreatePlayer();
            this.gameHUDInstance.PlayerShipList = playerShipList;

            if (FlatRedBall.Input.InputManager.Xbox360GamePads[0].IsConnected == false)
            {
                KeyboardButtonMap buttonMap = new KeyboardButtonMap();
                //MouseState mouseMap = new MouseState();

                // Here's how we'd assign the left and right:
                buttonMap.LeftAnalogUp = Keys.W;
                buttonMap.LeftAnalogLeft = Keys.A;
                buttonMap.LeftAnalogDown = Keys.S;
                buttonMap.LeftAnalogRight = Keys.D;

                buttonMap.RightAnalogUp = Keys.Up;
                buttonMap.RightAnalogLeft = Keys.Left;
                buttonMap.RightAnalogDown = Keys.Down;
                buttonMap.RightAnalogRight = Keys.Right;
                    
                //var rightStickPostion = new Vector3(mouseMap.X, mouseMap.Y, 0);
                //buttonMap.RightAnalogLeft = rightStick.X;
                //buttonMap.RightAnalogUp = rightStick.Y;                

                // Continue assigning the keys you want to use here:
                buttonMap.A = Keys.Space;

                // And now tell the 1st controller to use this button map
                InputManager.Xbox360GamePads[0].ButtonMap = buttonMap;
            }
        }

		void CustomActivity(bool firstTimeCalled)
		{
            Collisions();
            FindAngleToPlayer();
            UpdateHUD();
            	
        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }
        private void UpdateHUD()
        {
            this.gameHUDInstance.enemyTypeDisplayText = GlobalData.EnemyData.enemyType;
            this.gameHUDInstance.enemiesAliveDisplayText = GlobalData.EnemyData.enemiesKilled;
            this.gameHUDInstance.enemiesSpawnedDisplayText = GlobalData.EnemyData.enemiesSpawned;
            this.gameHUDInstance.waveCounterDisplayText = GlobalData.EnemyData.waveCounter;

        }
        private void Collisions()
        {
            if (playerShipList.Count > 0)
            {
                playerShipList[0].CollisionMesh.CollideAgainstBounce(gameScreenCollisionMesh, 0, 10, 10);
                EnemyVsPlayer();
            }            
            BoundsCollision();
            BulletVsEnemy();
            //EnemyVsEnemy(); //ToDo: performance decrease on high object counts.  Correct by adding waves
                   //in lieu of static spawn rates or partitioning enemies into multiple lists.
            
            //ToDo: add when adding enemies that shoot
            //BulletVsPlayer();

        }
        private void BoundsCollision() //combine these lists for optimization?
        {
            for (int i = bulletList.Count - 1; i > -1; i--)
            {
                bullet _bullet = bulletList[i];
                if (_bullet.CollisionMesh.CollideAgainst(gameScreenCollisionMesh))
                {
                    _bullet.Destroy();
                }
            }

            for (int j = enemyShipList.Count - 1; j > -1; j--)
            {
                enemyShip _enemyShip = enemyShipList[j];
                if (_enemyShip.CollisionMesh.CollideAgainst(gameScreenCollisionMesh))
                {
                    _enemyShip.Destroy();
                }
            }

            for (int k = speederList.Count - 1; k > -1; k--)
            {
                speeder _speeder = speederList[k];
                if (_speeder.CollisionMesh.CollideAgainst(gameScreenCollisionMesh))
                {
                    _speeder.Destroy();
                    GlobalData.EnemyData.enemiesKilled++;
                }
            }

        }

        private void BulletVsEnemy()
        {
            for (int i = bulletList.Count - 1; i > -1; i--)
            {
                bullet _bullet = bulletList[i];
                for (int j = enemyShipList.Count - 1; j > -1; j--)
                {
                    enemyShip _enemyShip = enemyShipList[j];
                    if (_enemyShip.CollisionMesh.CollideAgainst(_bullet.CollisionMesh))
                    {
                        _bullet.Destroy();
                        _enemyShip.Destroy();

                        GlobalData.PlayerData.score += _enemyShip.pointsWorth;
                        this.gameHUDInstance.scoreDisplayText = GlobalData.PlayerData.score;

                        GlobalData.EnemyData.enemiesKilled++; 

                        break;
                    }
                }
            }
        }

        private void EnemyVsPlayer()
        {
            for (int i = enemyShipList.Count - 1; i > -1; i--)
            {
                enemyShip _enemyShip = enemyShipList[i];
                playerShip _playerShip = playerShipList[0];

                if (_enemyShip.CollisionMesh.CollideAgainst(_playerShip.CollisionMesh))
                {
                    _playerShip.HP--;
                    _enemyShip.Destroy();
                    GlobalData.EnemyData.enemiesKilled++;
                    break;
                }
            }
        }

        private void EnemyVsEnemy()
        {
            for (int i = enemyShipList.Count - 1; i > -1; i--)
            {
                enemyShip _enemyShip1 = enemyShipList[i];
                for (int j = enemyShipList.Count - 1; j > -1; j--)
                {
                    if (j != i)
                    {
                        enemyShip _enemyShip2 = enemyShipList[j];
                        /*bounce each other*/
                        _enemyShip1.CollisionMesh.CollideAgainstMove(_enemyShip2.CollisionMesh, 10, 10);
                    }
                }
            }
        }

        private void FindAngleToPlayer()
        {
            Vector3 angleToPlayer = new Vector3(0,0,0);
            Vector3 currentPlayerPosition;

            for (int i = 0; i < enemyShipList.Count; i++)
            {
                if (playerShipList.Count > 0)
                {
                    currentPlayerPosition = playerShipList[0].Position;
                }
                else
                {
                    //On player death
                    currentPlayerPosition = new Vector3(0, 0, 0);
                }
                enemyShip _enemyShip = enemyShipList[i];
                angleToPlayer = currentPlayerPosition - enemyShipList[i].Position;
                angleToPlayer.Normalize();
                enemyShipList[i].Velocity = angleToPlayer * enemyShipList[i].movementSpeed;
            }
        }

        private void CreatePlayer()
        {
            playerShip _playerShip = new playerShip(ContentManagerName);
            playerShipList.Add(_playerShip);
        }
	}
}
