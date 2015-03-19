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
using SpaceFighterFRB.Entities;
using SpaceFighterFRB.Factories;
using FlatRedBall;
using FlatRedBall.Screens;
using System;
using System.Collections.Generic;
using System.Text;

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
	public partial class gameHUD : PositionedObject, IDestroyable
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
		
		private SpaceFighterFRB.Entities.healthBar healthBarInstance;
		private FlatRedBall.Graphics.Text score;
		private FlatRedBall.Graphics.Text enemyType;
		private FlatRedBall.Graphics.Text enemiesSpawned;
		private FlatRedBall.Graphics.Text enemiesAlive;
		private FlatRedBall.Graphics.Text waveCounter;
		public int scoreDisplayText
		{
			get
			{
				return int.Parse(score.DisplayText);
			}
			set
			{
				score.DisplayText = value.ToString();
			}
		}
		public int enemyTypeDisplayText
		{
			get
			{
				return int.Parse(enemyType.DisplayText);
			}
			set
			{
				enemyType.DisplayText = value.ToString();
			}
		}
		public int enemiesSpawnedDisplayText
		{
			get
			{
				return int.Parse(enemiesSpawned.DisplayText);
			}
			set
			{
				enemiesSpawned.DisplayText = value.ToString();
			}
		}
		public int enemiesAliveDisplayText
		{
			get
			{
				return int.Parse(enemiesAlive.DisplayText);
			}
			set
			{
				enemiesAlive.DisplayText = value.ToString();
			}
		}
		public int waveCounterDisplayText
		{
			get
			{
				return int.Parse(waveCounter.DisplayText);
			}
			set
			{
				waveCounter.DisplayText = value.ToString();
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public gameHUD()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public gameHUD(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public gameHUD(string contentManagerName, bool addToManagers) :
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
			healthBarInstance = new SpaceFighterFRB.Entities.healthBar(ContentManagerName, false);
			healthBarInstance.Name = "healthBarInstance";
			score = new FlatRedBall.Graphics.Text();
			score.Name = "score";
			enemyType = new FlatRedBall.Graphics.Text();
			enemyType.Name = "enemyType";
			enemiesSpawned = new FlatRedBall.Graphics.Text();
			enemiesSpawned.Name = "enemiesSpawned";
			enemiesAlive = new FlatRedBall.Graphics.Text();
			enemiesAlive.Name = "enemiesAlive";
			waveCounter = new FlatRedBall.Graphics.Text();
			waveCounter.Name = "waveCounter";
			
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
			healthBarInstance.ReAddToManagers(LayerProvidedByContainer);
			TextManager.AddToLayer(score, LayerProvidedByContainer);
			if (score.Font != null)
			{
				score.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemyType, LayerProvidedByContainer);
			if (enemyType.Font != null)
			{
				enemyType.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemiesSpawned, LayerProvidedByContainer);
			if (enemiesSpawned.Font != null)
			{
				enemiesSpawned.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemiesAlive, LayerProvidedByContainer);
			if (enemiesAlive.Font != null)
			{
				enemiesAlive.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(waveCounter, LayerProvidedByContainer);
			if (waveCounter.Font != null)
			{
				waveCounter.SetPixelPerfectScale(LayerProvidedByContainer);
			}
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			healthBarInstance.AddToManagers(LayerProvidedByContainer);
			TextManager.AddToLayer(score, LayerProvidedByContainer);
			if (score.Font != null)
			{
				score.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemyType, LayerProvidedByContainer);
			if (enemyType.Font != null)
			{
				enemyType.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemiesSpawned, LayerProvidedByContainer);
			if (enemiesSpawned.Font != null)
			{
				enemiesSpawned.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(enemiesAlive, LayerProvidedByContainer);
			if (enemiesAlive.Font != null)
			{
				enemiesAlive.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			TextManager.AddToLayer(waveCounter, LayerProvidedByContainer);
			if (waveCounter.Font != null)
			{
				waveCounter.SetPixelPerfectScale(LayerProvidedByContainer);
			}
			AddToManagersBottomUp(layerToAddTo);
			CustomInitialize();
		}

		public virtual void Activity()
		{
			// Generated Activity
			
			healthBarInstance.Activity();
			CustomActivity();
			
			// After Custom Activity
		}

		public virtual void Destroy()
		{
			// Generated Destroy
			SpriteManager.RemovePositionedObject(this);
			
			if (healthBarInstance != null)
			{
				healthBarInstance.Destroy();
				healthBarInstance.Detach();
			}
			if (score != null)
			{
				TextManager.RemoveText(score);
			}
			if (enemyType != null)
			{
				TextManager.RemoveText(enemyType);
			}
			if (enemiesSpawned != null)
			{
				TextManager.RemoveText(enemiesSpawned);
			}
			if (enemiesAlive != null)
			{
				TextManager.RemoveText(enemiesAlive);
			}
			if (waveCounter != null)
			{
				TextManager.RemoveText(waveCounter);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (healthBarInstance.Parent == null)
			{
				healthBarInstance.CopyAbsoluteToRelative();
				healthBarInstance.AttachTo(this, false);
			}
			if (healthBarInstance.Parent == null)
			{
				healthBarInstance.X = -650f;
			}
			else
			{
				healthBarInstance.RelativeX = -650f;
			}
			if (healthBarInstance.Parent == null)
			{
				healthBarInstance.Y = 100f;
			}
			else
			{
				healthBarInstance.RelativeY = 100f;
			}
			if (score.Parent == null)
			{
				score.CopyAbsoluteToRelative();
				score.AttachTo(this, false);
			}
			score.DisplayText = "";
			if (score.Parent == null)
			{
				score.X = -750f;
			}
			else
			{
				score.RelativeX = -750f;
			}
			if (score.Parent == null)
			{
				score.Y = 100f;
			}
			else
			{
				score.RelativeY = 100f;
			}
			if (enemyType.Parent == null)
			{
				enemyType.CopyAbsoluteToRelative();
				enemyType.AttachTo(this, false);
			}
			enemyType.DisplayText = "";
			if (enemyType.Parent == null)
			{
				enemyType.X = -750f;
			}
			else
			{
				enemyType.RelativeX = -750f;
			}
			if (enemyType.Parent == null)
			{
				enemyType.Y = 75f;
			}
			else
			{
				enemyType.RelativeY = 75f;
			}
			if (enemiesSpawned.Parent == null)
			{
				enemiesSpawned.CopyAbsoluteToRelative();
				enemiesSpawned.AttachTo(this, false);
			}
			enemiesSpawned.DisplayText = "";
			if (enemiesSpawned.Parent == null)
			{
				enemiesSpawned.X = -750f;
			}
			else
			{
				enemiesSpawned.RelativeX = -750f;
			}
			if (enemiesSpawned.Parent == null)
			{
				enemiesSpawned.Y = 125f;
			}
			else
			{
				enemiesSpawned.RelativeY = 125f;
			}
			enemiesSpawned.Blue = 1f;
			#if FRB_MDX
			enemiesSpawned.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			enemiesSpawned.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			if (enemiesAlive.Parent == null)
			{
				enemiesAlive.CopyAbsoluteToRelative();
				enemiesAlive.AttachTo(this, false);
			}
			enemiesAlive.DisplayText = "";
			if (enemiesAlive.Parent == null)
			{
				enemiesAlive.X = -750f;
			}
			else
			{
				enemiesAlive.RelativeX = -750f;
			}
			if (enemiesAlive.Parent == null)
			{
				enemiesAlive.Y = 150f;
			}
			else
			{
				enemiesAlive.RelativeY = 150f;
			}
			#if FRB_MDX
			enemiesAlive.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			enemiesAlive.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			enemiesAlive.Green = 1f;
			if (waveCounter.Parent == null)
			{
				waveCounter.CopyAbsoluteToRelative();
				waveCounter.AttachTo(this, false);
			}
			if (waveCounter.Parent == null)
			{
				waveCounter.X = -600f;
			}
			else
			{
				waveCounter.RelativeX = -600f;
			}
			if (waveCounter.Parent == null)
			{
				waveCounter.Y = 100f;
			}
			else
			{
				waveCounter.RelativeY = 100f;
			}
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			healthBarInstance.RemoveFromManagers();
			if (score != null)
			{
				TextManager.RemoveTextOneWay(score);
			}
			if (enemyType != null)
			{
				TextManager.RemoveTextOneWay(enemyType);
			}
			if (enemiesSpawned != null)
			{
				TextManager.RemoveTextOneWay(enemiesSpawned);
			}
			if (enemiesAlive != null)
			{
				TextManager.RemoveTextOneWay(enemiesAlive);
			}
			if (waveCounter != null)
			{
				TextManager.RemoveTextOneWay(waveCounter);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
				healthBarInstance.AssignCustomVariables(true);
			}
			if (healthBarInstance.Parent == null)
			{
				healthBarInstance.X = -650f;
			}
			else
			{
				healthBarInstance.RelativeX = -650f;
			}
			if (healthBarInstance.Parent == null)
			{
				healthBarInstance.Y = 100f;
			}
			else
			{
				healthBarInstance.RelativeY = 100f;
			}
			score.DisplayText = "";
			if (score.Parent == null)
			{
				score.X = -750f;
			}
			else
			{
				score.RelativeX = -750f;
			}
			if (score.Parent == null)
			{
				score.Y = 100f;
			}
			else
			{
				score.RelativeY = 100f;
			}
			enemyType.DisplayText = "";
			if (enemyType.Parent == null)
			{
				enemyType.X = -750f;
			}
			else
			{
				enemyType.RelativeX = -750f;
			}
			if (enemyType.Parent == null)
			{
				enemyType.Y = 75f;
			}
			else
			{
				enemyType.RelativeY = 75f;
			}
			enemiesSpawned.DisplayText = "";
			if (enemiesSpawned.Parent == null)
			{
				enemiesSpawned.X = -750f;
			}
			else
			{
				enemiesSpawned.RelativeX = -750f;
			}
			if (enemiesSpawned.Parent == null)
			{
				enemiesSpawned.Y = 125f;
			}
			else
			{
				enemiesSpawned.RelativeY = 125f;
			}
			enemiesSpawned.Blue = 1f;
			#if FRB_MDX
			enemiesSpawned.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			enemiesSpawned.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			enemiesAlive.DisplayText = "";
			if (enemiesAlive.Parent == null)
			{
				enemiesAlive.X = -750f;
			}
			else
			{
				enemiesAlive.RelativeX = -750f;
			}
			if (enemiesAlive.Parent == null)
			{
				enemiesAlive.Y = 150f;
			}
			else
			{
				enemiesAlive.RelativeY = 150f;
			}
			#if FRB_MDX
			enemiesAlive.ColorOperation = Microsoft.DirectX.Direct3D.TextureOperation.ColorTextureAlpha;
			#else
			enemiesAlive.ColorOperation = FlatRedBall.Graphics.ColorOperation.ColorTextureAlpha;
			#endif
			enemiesAlive.Green = 1f;
			if (waveCounter.Parent == null)
			{
				waveCounter.X = -600f;
			}
			else
			{
				waveCounter.RelativeX = -600f;
			}
			if (waveCounter.Parent == null)
			{
				waveCounter.Y = 100f;
			}
			else
			{
				waveCounter.RelativeY = 100f;
			}
			scoreDisplayText = 0;
			enemyTypeDisplayText = 0;
			enemiesSpawnedDisplayText = 0;
			enemiesAliveDisplayText = 0;
			waveCounterDisplayText = 0;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			healthBarInstance.ConvertToManuallyUpdated();
			TextManager.ConvertToManuallyUpdated(score);
			TextManager.ConvertToManuallyUpdated(enemyType);
			TextManager.ConvertToManuallyUpdated(enemiesSpawned);
			TextManager.ConvertToManuallyUpdated(enemiesAlive);
			TextManager.ConvertToManuallyUpdated(waveCounter);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("gameHUDStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			SpaceFighterFRB.Entities.healthBar.LoadStaticContent(contentManagerName);
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("gameHUDStaticUnload", UnloadStaticContent);
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
			}
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
		protected bool mIsPaused;
		public override void Pause (FlatRedBall.Instructions.InstructionList instructions)
		{
			base.Pause(instructions);
			mIsPaused = true;
		}
		public virtual void SetToIgnorePausing ()
		{
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(this);
			healthBarInstance.SetToIgnorePausing();
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(score);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(enemyType);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(enemiesSpawned);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(enemiesAlive);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(waveCounter);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			healthBarInstance.MoveToLayer(layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(score);
			}
			TextManager.AddToLayer(score, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(enemyType);
			}
			TextManager.AddToLayer(enemyType, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(enemiesSpawned);
			}
			TextManager.AddToLayer(enemiesSpawned, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(enemiesAlive);
			}
			TextManager.AddToLayer(enemiesAlive, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(waveCounter);
			}
			TextManager.AddToLayer(waveCounter, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
