#if ANDROID
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif


using BitmapFont = FlatRedBall.Graphics.BitmapFont;

using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;

#if XNA4 || WINDOWS_8
using Color = Microsoft.Xna.Framework.Color;
#elif FRB_MDX
using Color = System.Drawing.Color;
#else
using Color = Microsoft.Xna.Framework.Graphics.Color;
#endif

#if FRB_XNA || SILVERLIGHT
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Microsoft.Xna.Framework.Media;
#endif

// Generated Usings
using SpaceFighterFRB.Entities;
using SpaceFighterFRB.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceFighterFRB.Screens
{
	public partial class menuScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		
		private SpaceFighterFRB.Entities.menuHUD menuHUDInstance;
		private SpaceFighterFRB.Entities.menuCursor menuCursorInstance;
		private SpaceFighterFRB.Entities.menuPlayBox menuPlayBoxInstance;
		private SpaceFighterFRB.Entities.menuExitBox menuExitBoxInstance;

		public menuScreen()
			: base("menuScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			menuHUDInstance = new SpaceFighterFRB.Entities.menuHUD(ContentManagerName, false);
			menuHUDInstance.Name = "menuHUDInstance";
			menuCursorInstance = new SpaceFighterFRB.Entities.menuCursor(ContentManagerName, false);
			menuCursorInstance.Name = "menuCursorInstance";
			menuPlayBoxInstance = new SpaceFighterFRB.Entities.menuPlayBox(ContentManagerName, false);
			menuPlayBoxInstance.Name = "menuPlayBoxInstance";
			menuExitBoxInstance = new SpaceFighterFRB.Entities.menuExitBox(ContentManagerName, false);
			menuExitBoxInstance.Name = "menuExitBoxInstance";
			
			
			PostInitialize();
			base.Initialize(addToManagers);
			if (addToManagers)
			{
				AddToManagers();
			}

        }
        
// Generated AddToManagers
		public override void AddToManagers ()
		{
			menuHUDInstance.AddToManagers(mLayer);
			menuCursorInstance.AddToManagers(mLayer);
			menuPlayBoxInstance.AddToManagers(mLayer);
			menuExitBoxInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				menuHUDInstance.Activity();
				menuCursorInstance.Activity();
				menuPlayBoxInstance.Activity();
				menuExitBoxInstance.Activity();
			}
			else
			{
			}
			base.Activity(firstTimeCalled);
			if (!IsActivityFinished)
			{
				CustomActivity(firstTimeCalled);
			}


				// After Custom Activity
				
            
		}

		public override void Destroy()
		{
			// Generated Destroy
			
			if (menuHUDInstance != null)
			{
				menuHUDInstance.Destroy();
				menuHUDInstance.Detach();
			}
			if (menuCursorInstance != null)
			{
				menuCursorInstance.Destroy();
				menuCursorInstance.Detach();
			}
			if (menuPlayBoxInstance != null)
			{
				menuPlayBoxInstance.Destroy();
				menuPlayBoxInstance.Detach();
			}
			if (menuExitBoxInstance != null)
			{
				menuExitBoxInstance.Destroy();
				menuExitBoxInstance.Detach();
			}

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (menuCursorInstance.Parent == null)
			{
				menuCursorInstance.X = -35f;
			}
			else
			{
				menuCursorInstance.RelativeX = -35f;
			}
			if (menuCursorInstance.Parent == null)
			{
				menuCursorInstance.Y = 60f;
			}
			else
			{
				menuCursorInstance.RelativeY = 60f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			menuHUDInstance.RemoveFromManagers();
			menuCursorInstance.RemoveFromManagers();
			menuPlayBoxInstance.RemoveFromManagers();
			menuExitBoxInstance.RemoveFromManagers();
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				menuHUDInstance.AssignCustomVariables(true);
				menuCursorInstance.AssignCustomVariables(true);
				menuPlayBoxInstance.AssignCustomVariables(true);
				menuExitBoxInstance.AssignCustomVariables(true);
			}
			if (menuCursorInstance.Parent == null)
			{
				menuCursorInstance.X = -35f;
			}
			else
			{
				menuCursorInstance.RelativeX = -35f;
			}
			if (menuCursorInstance.Parent == null)
			{
				menuCursorInstance.Y = 60f;
			}
			else
			{
				menuCursorInstance.RelativeY = 60f;
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			menuHUDInstance.ConvertToManuallyUpdated();
			menuCursorInstance.ConvertToManuallyUpdated();
			menuPlayBoxInstance.ConvertToManuallyUpdated();
			menuExitBoxInstance.ConvertToManuallyUpdated();
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			#if DEBUG
			if (contentManagerName == FlatRedBallServices.GlobalContentManager)
			{
				HasBeenLoadedWithGlobalContentManager = true;
			}
			else if (HasBeenLoadedWithGlobalContentManager)
			{
				throw new Exception("This type has been loaded with a Global content manager, then loaded with a non-global.  This can lead to a lot of bugs");
			}
			#endif
			SpaceFighterFRB.Entities.menuHUD.LoadStaticContent(contentManagerName);
			SpaceFighterFRB.Entities.menuCursor.LoadStaticContent(contentManagerName);
			SpaceFighterFRB.Entities.menuPlayBox.LoadStaticContent(contentManagerName);
			SpaceFighterFRB.Entities.menuExitBox.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			return null;
		}
		public static object GetFile (string memberName)
		{
			return null;
		}
		object GetMember (string memberName)
		{
			return null;
		}


	}
}
