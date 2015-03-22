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
#endif
#endregion

namespace SpaceFighterFRB.Screens
{
	public partial class menuScreen
	{
        void CustomInitialize()
		{
            if (FlatRedBall.Input.InputManager.Xbox360GamePads[0].IsConnected == false)
            {
                KeyboardButtonMap buttonMap = new KeyboardButtonMap();

                // Here's how we'd assign the left and right:
                buttonMap.LeftAnalogUp = Keys.Up;
                buttonMap.LeftAnalogDown = Keys.Down;
                buttonMap.A = Keys.Enter;
                buttonMap.Back = Keys.Escape;

                // Continue assigning the keys you want to use here:

                // And now tell the 1st controller to use this button map
                InputManager.Xbox360GamePads[0].ButtonMap = buttonMap;
            }
        }

        void CustomActivity(bool firstTimeCalled)
        {
            DetectManualExit();

            //ToDo: Add Menu Music

            if (GlobalData.MenuData.AButton == true)
            {
                CheckCollision();
            }

        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void CheckCollision()
        {
            if (menuCursorInstance.collisionMesh.CollideAgainst(menuPlayBoxInstance.collisionMesh))
            {
                GlobalData.MenuData.play = false;
                this.MoveToScreen(typeof(gameScreen).FullName);
            }
            if (menuCursorInstance.collisionMesh.CollideAgainst(menuExitBoxInstance.collisionMesh))
            {
                FlatRedBallServices.Game.Exit();
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
