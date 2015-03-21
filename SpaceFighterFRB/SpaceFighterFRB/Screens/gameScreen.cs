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

                //Movement:
                buttonMap.LeftAnalogUp = Keys.W;
                buttonMap.LeftAnalogLeft = Keys.A;
                buttonMap.LeftAnalogDown = Keys.S;
                buttonMap.LeftAnalogRight = Keys.D;

                //Shoot:
                buttonMap.RightAnalogUp = Keys.Up;
                buttonMap.RightAnalogLeft = Keys.Left;
                buttonMap.RightAnalogDown = Keys.Down;
                buttonMap.RightAnalogRight = Keys.Right;
                    
                //var rightStickPostion = new Vector3(mouseMap.X, mouseMap.Y, 0);
                //buttonMap.RightAnalogLeft = rightStick.X;
                //buttonMap.RightAnalogUp = rightStick.Y;                

                //Menu navigation and Exit:
                buttonMap.A = Keys.Space;
                buttonMap.Back = Keys.Escape;

                // And now tell the 1st controller to use this button map
                InputManager.Xbox360GamePads[0].ButtonMap = buttonMap;
            }
            GlobalData.MenuData.exit = false;
        }

		void CustomActivity(bool firstTimeCalled)
		{
            DetectManualExit();
            DetectCollisions();
            ActivateSpeeders();
            FindAngleToPlayer();
            UpdateHUD();
            DetectEndOfGame();
            	
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
        private void DetectCollisions()
        {
            if (playerShipList.Count > 0)
            {
                playerShipList[0].CollisionMesh.CollideAgainstBounce(gameScreenCollisionMesh, 0, 10, 10);
                CollisionEnemiesVsPlayer();
            }
            CollisionBounds();
            CollisionEnemiesVsBullet();
            CollisionEnemiesVsEnemy(); /*ToDo: performance decrease on high object counts.  Correct by adding waves
                   in lieu of static spawn rates or add partitioning of enemies into multiple lists.*/
            
            //ToDo: add when adding enemies that shoot
            //CollisionBulletsVsPlayer();

        }
        private void CollisionBounds()
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
                _speeder.CollisionMesh.CollideAgainstBounce(gameScreenCollisionMesh, 0, 1, 1);
            }
        }

        private void CollisionEnemiesVsBullet()
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
                for (int j = speederList.Count - 1; j > -1; j--)
                {
                    speeder _speeder = speederList[j];
                    if (_speeder.CollisionMesh.CollideAgainst(_bullet.CollisionMesh))
                    {
                        _bullet.Destroy();
                        _speeder.Destroy();
                        
                        GlobalData.PlayerData.score += _speeder.pointsWorth;
                        this.gameHUDInstance.scoreDisplayText = GlobalData.PlayerData.score;

                        GlobalData.EnemyData.enemiesKilled++;

                        break;
                    }
                }
            }

        }

        private void CollisionEnemiesVsPlayer()
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
            for (int i = speederList.Count - 1; i > -1; i--)
            {
                speeder _speeder = speederList[i];
                playerShip _playerShip = playerShipList[0];

                if (_speeder.CollisionMesh.CollideAgainst(_playerShip.CollisionMesh))
                {
                    _playerShip.HP--;
                    _speeder.Destroy();
                    GlobalData.EnemyData.enemiesKilled++;
                    break;
                }
            }
        }

        private void CollisionEnemiesVsEnemy()
        {
            /*For now I like this wave of enemies stacking on top of one another.*/
            //for (int i = enemyShipList.Count - 1; i > -1; i--)
            //{
            //    enemyShip _enemyShip1 = enemyShipList[i];
            //    for (int j = enemyShipList.Count - 1; j > -1; j--)
            //    {
            //        if (j != i)
            //        {
            //            enemyShip _enemyShip2 = enemyShipList[j];
            //            /*bounce each other*/
            //            _enemyShip1.CollisionMesh.CollideAgainstMove(_enemyShip2.CollisionMesh, 1, 1);
            //        }
            //    }
            //}
            for (int i = speederList.Count - 1; i > -1; i--)
            {
                speeder _speeder1 = speederList[i];
                for (int j = speederList.Count - 1; j > -1; j--)
                {
                    if (j != i)
                    {
                        speeder _speeder2 = speederList[j];
                        /*bounce each other*/
                        _speeder1.CollisionMesh.CollideAgainstBounce(_speeder2.CollisionMesh, 1, 1, 1);
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

        private void ActivateSpeeders()
        {
            for (int i = 0; i < speederList.Count; i++)
            {
                speeder _speeder = speederList[i];

                if (TimeManager.CurrentTime - _speeder.spawnTime >= 1)
                {
                    var direction = _speeder.Velocity;
                    direction.Normalize();
                    _speeder.CurrentState = speeder.VariableState.speeding;
                    _speeder.Velocity = direction * _speeder.movementSpeed;
                    _speeder.spawnTime = 0;  //Naive??  This forces it to only run once.
                }
            }
        }

        private void CreatePlayer()
        {
            playerShip _playerShip = new playerShip(ContentManagerName);
            playerShipList.Add(_playerShip);
        }
        
        void DetectEndOfGame()
        {
            if(gameOverHUDInstance.Visible == false && this.playerShipList.Count == 0)
            {
                gameOverHUDInstance.Visible = true;
            }
        }

        void DetectManualExit()
        {
            if (GlobalData.MenuData.exit == true)
            {
                FlatRedBallServices.Game.Exit();
            }
        }
	}
}
