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
	public partial class speeder : PositionedObject, IDestroyable, IPoolable, FlatRedBall.Math.Geometry.ICollidable
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
		public enum VariableState
		{
			Uninitialized = 0, //This exists so that the first set call actually does something
			Unknown = 1, //This exists so that if the entity is actually a child entity and has set a child state, you will get this
			spawning = 2, 
			speeding = 3
		}
		protected int mCurrentState = 0;
		public Entities.speeder.VariableState CurrentState
		{
			get
			{
				if (Enum.IsDefined(typeof(VariableState), mCurrentState))
				{
					return (VariableState)mCurrentState;
				}
				else
				{
					return VariableState.Unknown;
				}
			}
			set
			{
				mCurrentState = (int)value;
				switch(CurrentState)
				{
					case  VariableState.Uninitialized:
						break;
					case  VariableState.Unknown:
						break;
					case  VariableState.spawning:
						movementSpeed = 1f;
						pointsWorth = 0;
						SpriteBlue = 0.565f;
						SpriteGreen = 0.933f;
						SpriteRed = 0.565f;
						SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
						break;
					case  VariableState.speeding:
						movementSpeed = 200f;
						pointsWorth = 1;
						SpriteBlue = 0f;
						SpriteGreen = 0f;
						SpriteRed = 1f;
						SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		protected static Microsoft.Xna.Framework.Graphics.Texture2D enemyShip2;
		protected static Microsoft.Xna.Framework.Graphics.Texture2D enemyShip2b;
		
		private FlatRedBall.Sprite Sprite;
		private FlatRedBall.Math.Geometry.Circle mCollisionMesh;
		public FlatRedBall.Math.Geometry.Circle CollisionMesh
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
		public float movementSpeed;
		public int pointsWorth;
		public float SpriteBlue
		{
			get
			{
				return Sprite.Blue;
			}
			set
			{
				Sprite.Blue = value;
			}
		}
		public float SpriteGreen
		{
			get
			{
				return Sprite.Green;
			}
			set
			{
				Sprite.Green = value;
			}
		}
		public float SpriteRed
		{
			get
			{
				return Sprite.Red;
			}
			set
			{
				Sprite.Red = value;
			}
		}
		public FlatRedBall.Graphics.ColorOperation SpriteColorOperation
		{
			get
			{
				return Sprite.ColorOperation;
			}
			set
			{
				Sprite.ColorOperation = value;
			}
		}
		public System.Double spawnTime;
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

        public speeder()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public speeder(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public speeder(string contentManagerName, bool addToManagers) :
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
			Sprite = new FlatRedBall.Sprite();
			Sprite.Name = "Sprite";
			mCollisionMesh = new FlatRedBall.Math.Geometry.Circle();
			mCollisionMesh.Name = "mCollisionMesh";
			mGeneratedCollision = new FlatRedBall.Math.Geometry.ShapeCollection();
			mGeneratedCollision.Circles.AddOneWay(mCollisionMesh);
			
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
			ShapeManager.AddToLayer(mCollisionMesh, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(Sprite, LayerProvidedByContainer);
			ShapeManager.AddToLayer(mCollisionMesh, LayerProvidedByContainer);
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
				speederFactory.MakeUnused(this, false);
			}
			
			if (Sprite != null)
			{
				SpriteManager.RemoveSpriteOneWay(Sprite);
			}
			if (CollisionMesh != null)
			{
				ShapeManager.RemoveOneWay(CollisionMesh);
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
			Sprite.Texture = enemyShip2b;
			Sprite.TextureScale = 0.55f;
			if (mCollisionMesh.Parent == null)
			{
				mCollisionMesh.CopyAbsoluteToRelative();
				mCollisionMesh.AttachTo(this, false);
			}
			CollisionMesh.Radius = 8.75f;
			if (CollisionMesh.Parent == null)
			{
				CollisionMesh.X = 0f;
			}
			else
			{
				CollisionMesh.RelativeX = 0f;
			}
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
				ShapeManager.RemoveOneWay(CollisionMesh);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			Sprite.Texture = enemyShip2b;
			Sprite.TextureScale = 0.55f;
			mCollisionMesh.Radius = 8.75f;
			if (mCollisionMesh.Parent == null)
			{
				mCollisionMesh.X = 0f;
			}
			else
			{
				mCollisionMesh.RelativeX = 0f;
			}
			mCollisionMesh.Visible = false;
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("speederStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/speeder/enemyship2.png", ContentManagerName))
				{
					registerUnload = true;
				}
				enemyShip2 = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/speeder/enemyship2.png", ContentManagerName);
				if (!FlatRedBallServices.IsLoaded<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/speeder/enemyship2b.png", ContentManagerName))
				{
					registerUnload = true;
				}
				enemyShip2b = FlatRedBallServices.Load<Microsoft.Xna.Framework.Graphics.Texture2D>(@"content/entities/speeder/enemyship2b.png", ContentManagerName);
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("speederStaticUnload", UnloadStaticContent);
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
				if (enemyShip2 != null)
				{
					enemyShip2= null;
				}
				if (enemyShip2b != null)
				{
					enemyShip2b= null;
				}
			}
		}
		static VariableState mLoadingState = VariableState.Uninitialized;
		public static VariableState LoadingState
		{
			get
			{
				return mLoadingState;
			}
			set
			{
				mLoadingState = value;
			}
		}
		public FlatRedBall.Instructions.Instruction InterpolateToState (VariableState stateToInterpolateTo, double secondsToTake)
		{
			switch(stateToInterpolateTo)
			{
				case  VariableState.spawning:
					Sprite.BlueRate = (0.565f - Sprite.Blue) / (float)secondsToTake;
					Sprite.GreenRate = (0.933f - Sprite.Green) / (float)secondsToTake;
					Sprite.RedRate = (0.565f - Sprite.Red) / (float)secondsToTake;
					break;
				case  VariableState.speeding:
					Sprite.BlueRate = (0f - Sprite.Blue) / (float)secondsToTake;
					Sprite.GreenRate = (0f - Sprite.Green) / (float)secondsToTake;
					Sprite.RedRate = (1f - Sprite.Red) / (float)secondsToTake;
					break;
			}
			var instruction = new FlatRedBall.Instructions.DelegateInstruction<VariableState>(StopStateInterpolation, stateToInterpolateTo);
			instruction.TimeToExecute = FlatRedBall.TimeManager.CurrentTime + secondsToTake;
			this.Instructions.Add(instruction);
			return instruction;
		}
		public void StopStateInterpolation (VariableState stateToStop)
		{
			switch(stateToStop)
			{
				case  VariableState.spawning:
					Sprite.BlueRate =  0;
					Sprite.GreenRate =  0;
					Sprite.RedRate =  0;
					break;
				case  VariableState.speeding:
					Sprite.BlueRate =  0;
					Sprite.GreenRate =  0;
					Sprite.RedRate =  0;
					break;
			}
			CurrentState = stateToStop;
		}
		public void InterpolateBetween (VariableState firstState, VariableState secondState, float interpolationValue)
		{
			#if DEBUG
			if (float.IsNaN(interpolationValue))
			{
				throw new Exception("interpolationValue cannot be NaN");
			}
			#endif
			bool setmovementSpeed = true;
			float movementSpeedFirstValue= 0;
			float movementSpeedSecondValue= 0;
			bool setpointsWorth = true;
			int pointsWorthFirstValue= 0;
			int pointsWorthSecondValue= 0;
			bool setSpriteBlue = true;
			float SpriteBlueFirstValue= 0;
			float SpriteBlueSecondValue= 0;
			bool setSpriteGreen = true;
			float SpriteGreenFirstValue= 0;
			float SpriteGreenSecondValue= 0;
			bool setSpriteRed = true;
			float SpriteRedFirstValue= 0;
			float SpriteRedSecondValue= 0;
			switch(firstState)
			{
				case  VariableState.spawning:
					movementSpeedFirstValue = 1f;
					pointsWorthFirstValue = 0;
					SpriteBlueFirstValue = 0.565f;
					SpriteGreenFirstValue = 0.933f;
					SpriteRedFirstValue = 0.565f;
					if (interpolationValue < 1)
					{
						this.SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
					}
					break;
				case  VariableState.speeding:
					movementSpeedFirstValue = 200f;
					pointsWorthFirstValue = 1;
					SpriteBlueFirstValue = 0f;
					SpriteGreenFirstValue = 0f;
					SpriteRedFirstValue = 1f;
					if (interpolationValue < 1)
					{
						this.SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
					}
					break;
			}
			switch(secondState)
			{
				case  VariableState.spawning:
					movementSpeedSecondValue = 1f;
					pointsWorthSecondValue = 0;
					SpriteBlueSecondValue = 0.565f;
					SpriteGreenSecondValue = 0.933f;
					SpriteRedSecondValue = 0.565f;
					if (interpolationValue >= 1)
					{
						this.SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
					}
					break;
				case  VariableState.speeding:
					movementSpeedSecondValue = 200f;
					pointsWorthSecondValue = 1;
					SpriteBlueSecondValue = 0f;
					SpriteGreenSecondValue = 0f;
					SpriteRedSecondValue = 1f;
					if (interpolationValue >= 1)
					{
						this.SpriteColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
					}
					break;
			}
			if (setmovementSpeed)
			{
				movementSpeed = movementSpeedFirstValue * (1 - interpolationValue) + movementSpeedSecondValue * interpolationValue;
			}
			if (setpointsWorth)
			{
				pointsWorth = FlatRedBall.Math.MathFunctions.RoundToInt(pointsWorthFirstValue* (1 - interpolationValue) + pointsWorthSecondValue * interpolationValue);
			}
			if (setSpriteBlue)
			{
				SpriteBlue = SpriteBlueFirstValue * (1 - interpolationValue) + SpriteBlueSecondValue * interpolationValue;
			}
			if (setSpriteGreen)
			{
				SpriteGreen = SpriteGreenFirstValue * (1 - interpolationValue) + SpriteGreenSecondValue * interpolationValue;
			}
			if (setSpriteRed)
			{
				SpriteRed = SpriteRedFirstValue * (1 - interpolationValue) + SpriteRedSecondValue * interpolationValue;
			}
			if (interpolationValue < 1)
			{
				mCurrentState = (int)firstState;
			}
			else
			{
				mCurrentState = (int)secondState;
			}
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  VariableState.spawning:
					break;
				case  VariableState.speeding:
					break;
			}
		}
		[System.Obsolete("Use GetFile instead")]
		public static object GetStaticMember (string memberName)
		{
			switch(memberName)
			{
				case  "enemyShip2":
					return enemyShip2;
				case  "enemyShip2b":
					return enemyShip2b;
			}
			return null;
		}
		public static object GetFile (string memberName)
		{
			switch(memberName)
			{
				case  "enemyShip2":
					return enemyShip2;
				case  "enemyShip2b":
					return enemyShip2b;
			}
			return null;
		}
		object GetMember (string memberName)
		{
			switch(memberName)
			{
				case  "enemyShip2":
					return enemyShip2;
				case  "enemyShip2b":
					return enemyShip2b;
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
