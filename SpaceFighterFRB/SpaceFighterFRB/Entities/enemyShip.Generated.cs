#if ANDROID
#define REQUIRES_PRIMARY_THREAD_LOADING
#endif

using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
// Generated Usings
using SpaceFighterFRB.Screens;
using FlatRedBall.Graphics;
using FlatRedBall.Math;
using SpaceFighterFRB.Performance;
using SpaceFighterFRB.Entities;
using SpaceFighterFRB.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework.Graphics;

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
#endif

#if FRB_XNA && !MONODROID
using Model = Microsoft.Xna.Framework.Graphics.Model;
#endif

namespace SpaceFighterFRB.Entities
{
	public partial class enemyShip : PositionedObject, IDestroyable, IPoolable, FlatRedBall.Math.Geometry.ICollidable
	{
        // This is made global so that static lazy-loaded content can access it.
        public static string ContentManagerName
        {
            get;
            set;
        }

		// Generated Fields
		#if DEBUG
		static bool HasBeenLoadedWithGlobalContentManager = false;
		#endif
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static FlatRedBall.Math.Geometry.ShapeCollection collisionMesh;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D enemyShip1;
		
		private FlatRedBall.Sprite Sprite;
		private FlatRedBall.Math.Geometry.ShapeCollection mCollisionMesh;
		public FlatRedBall.Math.Geometry.ShapeCollection CollisionMesh
		{
			get
			{
				return mCollisionMesh;
			}
			private set
			{
				mCollisionMesh = value;
			}
		}
		public float movementSpeed = 100f;
		public int pointsWorth = 1;
		public int Index { get; set; }
		public bool Used { get; set; }
		private FlatRedBall.Math.Geometry.ShapeCollection mGeneratedCollision;
		public FlatRedBall.Math.Geometry.ShapeCollection Collision
		{
			get
			{
				return mGeneratedCollision;
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public enemyShip()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public enemyShip(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public enemyShip(string contentManagerName, bool addToManagers) :
			base()
		{
			// Don't delete this:
            ContentManagerName = contentManagerName;
            InitializeEntity(addToManagers);

		}

		protected virtual void InitializeEntity(bool addToManagers)
		{
			// Generated Initialize
			LoadStaticContent(ContentManagerName);
			mCollisionMesh = collisionMesh.Clone();
			Sprite = new FlatRedBall.Sprite();
			Sprite.Name = "Sprite";
			mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
			
			PostInitialize();
			if (addToManagers)
			{
				AddToManagers(null);
			}


		}

// Generated AddToManagers
		public virtual void ReAddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			mCollisionMesh.AddToManagers(LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			mCollisionMesh.AddToManagers(LayerProvidedByContainer);
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			if (Used)
			{
				enemyShipFactory.MakeUnused(this, false);
			}
			
			if (Sprite != null)
			{
				SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (CollisionMesh != null)
			{
				CollisionMesh.RemoveFromManagers(false);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (Sprite.Parent == null)
			{
				Sprite.CopyAbsoluteToRelative();
				Sprite.AttachTo(this, false);
			}
			Sprite.Texture = enemyShip1;
			Sprite.TextureScale = 1.5f;
			Sprite.Blue = 0f;
			#if FRB_MDX
			Sprite.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			Sprite.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			Sprite.Green = 1f;
			Sprite.Red = 0f;
			mCollisionMesh.CopyAbsoluteToRelative(false);
			mCollisionMesh.AttachAllDetachedTo(this, false);
			CollisionMesh.Visible = false;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			if (Sprite != null)
			{
				SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (CollisionMesh != null)
			{
				CollisionMesh.RemoveFromManagers(false);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			Sprite.Texture = enemyShip1;
			Sprite.TextureScale = 1.5f;
			Sprite.Blue = 0f;
			#if FRB_MDX
			Sprite.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			Sprite.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			Sprite.Green = 1f;
			Sprite.Red = 0f;
			mCollisionMesh.Visible = false;
			movementSpeed = 100f;
			pointsWorth = 1;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(Sprite);
		}
		public static void LoadStaticContent (string contentManagerName)
		{
			if (string.IsNullOrEmpty(contentManagerName))
			{
				throw new ArgumentException("contentManagerName cannot be empty or null");
			}
			ContentManagerName = contentManagerName;
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
			bool registerUnload = false;
			if (LoadedContentManagers.Contains(contentManagerName) == false)
			{
				LoadedContentManagers.Add(contentManagerName);
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("enemyShipStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/enemyship/collisionmesh.shcx", ContentManagerName))
				{
					registerUnload = true;
				}
				collisionMesh = FlatRedBallServices.Load<FlatRedBall.Math.Geometry.ShapeCollection>(@"content/entities/enemyship/collisionmesh.shcx", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/enemyship/enemyship1.png", ContentManagerName))
				{
					registerUnload = true;
				}
				enemyShip1 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/enemyship/enemyship1.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("enemyShipStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			CustomLoadStaticContent(contentManagerName);
		}
		public static void UnloadStaticContent ()
		{
			if (LoadedContentManagers.Count != 0)
			{
				LoadedContentManagers.RemoveAt(0);
				mRegisteredUnloads.RemoveAt(0);
			}
			if (LoadedContentManagers.Count == 0)
			{
				if (collisionMesh != null)
				{
					collisionMesh.RemoveFromManagers(ContentManagerName != "Global");
					collisionMesh= null;
				}
				if (enemyShip1 != null)
				{
					enemyShip1= null;
				}
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "collisionMesh":
					return collisionMesh;
				case  "enemyShip1":
					return enemyShip1;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "collisionMesh":
					return collisionMesh;
				case  "enemyShip1":
					return enemyShip1;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "collisionMesh":
					return collisionMesh;
				case  "enemyShip1":
					return enemyShip1;
			}
			return null;
		}
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Sprite);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(CollisionMesh);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(Sprite);
			}
			SpriteManager.AddToLayer(Sprite, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
