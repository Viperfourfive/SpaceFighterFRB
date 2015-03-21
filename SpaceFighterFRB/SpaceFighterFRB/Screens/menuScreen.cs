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

                // Continue assigning the keys you want to use here:

                // And now tell the 1st controller to use this button map
                InputManager.Xbox360GamePads[0].ButtonMap = buttonMap;
            }

            GlobalData.MenuData.exit = false;
            GlobalData.MenuData.play = false;
        }

        void CustomActivity(bool firstTimeCalled)
        {
            //ToDo: Add Menu Music

            if (GlobalData.MenuData.play)
            {
                this.MoveToScreen(typeof(gameScreen).FullName);
            }
            if (GlobalData.MenuData.exit)
            {
                FlatRedBallServices.Game.Exit();
            }

        }

		void CustomDestroy()
		{


		}

        static void CustomLoadStaticContent(string contentManagerName)
        {


        }
	}
}
