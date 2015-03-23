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
	public partial class playerShip
	{
        int playerShipHP;
        public int HP
        {
            get
            {
                return playerShipHP;
            }
            set
            {
                playerShipHP = value;
                if (playerShipHP <= 0)
                {
                    GlobalData.PlayerData.position = this.Position;
                    GlobalData.PlayerData.rotation = this.lastRotationZ;
                    this.Destroy();
                }
            }
        }

        double leftMotorDuration;
        public double LeftVibrationDuration
        {
            get
            {
                return leftMotorDuration;
            }
            set
            {
                leftMotorDuration = value;
            }
        }

        double rightMotorDuration;
        public double RightVibrationDuration
        {
            get
            {
                return rightMotorDuration;
            }
            set
            {
                rightMotorDuration = value;
            }
        }

        Xbox360GamePad mGamePad;

        /// <summary>
        /// Initialization logic which is execute only one time for this Entity (unless the Entity is pooled).
        /// This method is called when the Entity is added to managers. Entities which are instantiated but not
        /// added to managers will not have this method called.
        /// </summary>
		private void CustomInitialize()
		{
            mGamePad = InputManager.Xbox360GamePads[0];
            HP = startingHP;

		}

		private void CustomActivity()
		{
            DetectManualExit();
            
            Move();
            DetectLeftMotor();
            DetectRightMotor();
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        void Move()
        {
            if (mGamePad.LeftStick.Position.X != 0)
            {
                this.Position.X += mGamePad.LeftStick.Position.X * movementSpeed;
            }
            if (mGamePad.LeftStick.Position.Y != 0)
            {
                this.Position.Y += mGamePad.LeftStick.Position.Y * movementSpeed;
            }
            if (mGamePad.RightStick.Position.X != 0 || mGamePad.RightStick.Position.Y != 0)
            {
                this.lastRotationZ = (float)mGamePad.RightStick.Angle - 1.57079633f;

                if (TimeManager.SecondsSince(lastShotFired) > startingFireRate)
                {
                    Shoot();
                    lastShotFired = TimeManager.CurrentTime;
                }
            }
            this.RotationZ = lastRotationZ;
        }

        void Shoot()
        {
                bullet _b = bulletFactory.CreateNew();
                _b.Position = this.Position;
                _b.Position += this.RotationMatrix.Up * 5;
                _b.RotationZ = this.RotationZ;
                _b.Velocity = this.RotationMatrix.Up * _b.movementSpeed;

                this.rightMotorDuration = TimeManager.CurrentTime;
        }

        void DetectLeftMotor()
        {
            if(TimeManager.SecondsSince(leftMotorDuration) <= 1)
            {
                mGamePad.SetVibration(1f, 0f);
            }
            else
            {
                TurnOffVibration();
            }
        }

        void DetectRightMotor()
        {
            if (TimeManager.SecondsSince(rightMotorDuration) <= .2)
            {
                mGamePad.SetVibration(0f, 1f);
            }
            else
            {
                TurnOffVibration();
            }
        }

        void TurnOffVibration()
        {
            mGamePad.SetVibration(0f, 0f);
        }

        void DetectManualExit()
        {
            if (mGamePad.ButtonDown(Xbox360GamePad.Button.Back))
            {
                FlatRedBallServices.Game.Exit();
            }
        }
	}
}
