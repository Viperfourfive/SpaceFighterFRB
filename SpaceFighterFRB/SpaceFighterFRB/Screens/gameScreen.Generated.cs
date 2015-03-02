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
using FlatRedBall.Math;
using FlatRedBall.Math.Geometry;

namespace SpaceFighterFRB.Screens
{
	public partial class gameScreen : Screen
	{
		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		protected static FlatRedBall.Math.Geometry.ShapeCollection gameScreenCollisionMesh;
		
		private PositionedObjectList<SpaceFighterFRB.Entities.bullet> bulletList;
		private PositionedObjectList<SpaceFighterFRB.Entities.enemyShip> enemyShipList;
		private SpaceFighterFRB.Entities.gameHUD gameHUDInstance;
		private SpaceFighterFRB.Entities.enemySpawner enemySpawnerInstance;
		private PositionedObjectList<SpaceFighterFRB.Entities.playerShip> playerShipList;

		public gameScreen()
			: base("gameScreen")
		{
		}

        public override void Initialize(bool addToManagers)
        {
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			bulletList = new PositionedObjectList<SpaceFighterFRB.Entities.bullet>();
			bulletList.Name = "bulletList";
			enemyShipList = new PositionedObjectList<SpaceFighterFRB.Entities.enemyShip>();
			enemyShipList.Name = "enemyShipList";
			gameHUDInstance = new SpaceFighterFRB.Entities.gameHUD(ContentManagerName, false);
			gameHUDInstance.Name = "gameHUDInstance";
			enemySpawnerInstance = new SpaceFighterFRB.Entities.enemySpawner(ContentManagerName, false);
			enemySpawnerInstance.Name = "enemySpawnerInstance";
			playerShipList = new PositionedObjectList<SpaceFighterFRB.Entities.playerShip>();
			playerShipList.Name = "playerShipList";
			
			
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
			gameScreenCollisionMesh.AddToManagers(mLayer);
			bulletFactory.Initialize(bulletList, ContentManagerName);
			enemyShipFactory.Initialize(enemyShipList, ContentManagerName);
			gameHUDInstance.AddToManagers(mLayer);
			enemySpawnerInstance.AddToManagers(mLayer);
			base.AddToManagers();
			AddToManagersBottomUp();
			CustomInitialize();
		}


		public override void Activity(bool firstTimeCalled)
		{
			// Generated Activity
			if (!IsPaused)
			{
				
				for (int i = bulletList.Count - 1; i > -1; i--)
				{
					if (i < bulletList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						bulletList[i].Activity();
					}
				}
				for (int i = enemyShipList.Count - 1; i > -1; i--)
				{
					if (i < enemyShipList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						enemyShipList[i].Activity();
					}
				}
				gameHUDInstance.Activity();
				enemySpawnerInstance.Activity();
				for (int i = playerShipList.Count - 1; i > -1; i--)
				{
					if (i < playerShipList.Count)
					{
						// We do the extra if-check because activity could destroy any number of entities
						playerShipList[i].Activity();
					}
				}
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
			bulletFactory.Destroy();
			enemyShipFactory.Destroy();
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				gameScreenCollisionMesh.RemoveFromManagers(ContentManagerName != "Global");
			}
			else
			{
				gameScreenCollisionMesh.RemoveFromManagers(false);
			}
			if (this.UnloadsContentManagerWhenDestroyed && ContentManagerName != "Global")
			{
				gameScreenCollisionMesh = null;
			}
			else
			{
				gameScreenCollisionMesh.MakeOneWay();
			}
			
			bulletList.MakeOneWay();
			enemyShipList.MakeOneWay();
			playerShipList.MakeOneWay();
			for (int i = bulletList.Count - 1; i > -1; i--)
			{
				bulletList[i].Destroy();
			}
			for (int i = enemyShipList.Count - 1; i > -1; i--)
			{
				enemyShipList[i].Destroy();
			}
			if (gameHUDInstance != null)
			{
				gameHUDInstance.Destroy();
				gameHUDInstance.Detach();
			}
			if (enemySpawnerInstance != null)
			{
				enemySpawnerInstance.Destroy();
				enemySpawnerInstance.Detach();
			}
			for (int i = playerShipList.Count - 1; i > -1; i--)
			{
				playerShipList[i].Destroy();
			}
			bulletList.MakeTwoWay();
			enemyShipList.MakeTwoWay();
			playerShipList.MakeTwoWay();

			base.Destroy();

			CustomDestroy();

		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp ()
		{
			CameraSetup.ResetCamera(SpriteManager.Camera);
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			for (int i = bulletList.Count - 1; i > -1; i--)
			{
				bulletList[i].Destroy();
			}
			for (int i = enemyShipList.Count - 1; i > -1; i--)
			{
				enemyShipList[i].Destroy();
			}
			gameHUDInstance.RemoveFromManagers();
			enemySpawnerInstance.RemoveFromManagers();
			for (int i = playerShipList.Count - 1; i > -1; i--)
			{
				playerShipList[i].Destroy();
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				gameHUDInstance.AssignCustomVariables(true);
				enemySpawnerInstance.AssignCustomVariables(true);
			}
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			for (int i = 0; i < bulletList.Count; i++)
			{
				bulletList[i].ConvertToManuallyUpdated();
			}
			for (int i = 0; i < enemyShipList.Count; i++)
			{
				enemyShipList[i].ConvertToManuallyUpdated();
			}
			gameHUDInstance.ConvertToManuallyUpdated();
			enemySpawnerInstance.ConvertToManuallyUpdated();
			for (int i = 0; i < playerShipList.Count; i++)
			{
				playerShipList[i].ConvertToManuallyUpdated();
			}
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
			if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/gamescreencollisionmesh.shcx", contentManagerName))
			{
			}
			gameScreenCollisionMesh = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/screens/gamescreen/gamescreencollisionmesh.shcx", contentManagerName);
			SpaceFighterFRB.Entities.gameHUD.LoadStaticContent(contentManagerName);
			SpaceFighterFRB.Entities.enemySpawner.LoadStaticContent(contentManagerName);
			CustomLoadStaticContent(contentManagerName);
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "gameScreenCollisionMesh":
					return gameScreenCollisionMesh;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "gameScreenCollisionMesh":
					return gameScreenCollisionMesh;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "gameScreenCollisionMesh":
					return gameScreenCollisionMesh;
			}
			return null;
		}


	}
}
