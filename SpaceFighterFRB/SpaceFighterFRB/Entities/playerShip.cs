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
                    this.Destroy();
                }
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
            Move();

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
                //another way
                //this.lastRotationZ = (float)Math.Atan2(-mGamePad.RightStick.Position.Y,
                //                                       -mGamePad.RightStick.Position.X) + 1.57079633f;
                Shoot();
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
        }


	}
}
