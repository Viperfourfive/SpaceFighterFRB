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
		private FlatRedBall.Graphics.Text Text;
		public int score
		{
			get
			{
				return int.Parse(Text.DisplayText);
			}
			set
			{
				Text.DisplayText = value.ToString();
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
			Text = new FlatRedBall.Graphics.Text();
			Text.Name = "Text";
			
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
			TextManager.AddToLayer(Text, LayerProvidedByContainer);
			if (Text.Font != null)
			{
				Text.SetPixelPerfectScale(LayerProvidedByContainer);
			}
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			healthBarInstance.AddToManagers(LayerProvidedByContainer);
			TextManager.AddToLayer(Text, LayerProvidedByContainer);
			if (Text.Font != null)
			{
				Text.SetPixelPerfectScale(LayerProvidedByContainer);
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
			if (Text != null)
			{
				TextManager.RemoveText(Text);
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
			if (Text.Parent == null)
			{
				Text.CopyAbsoluteToRelative();
				Text.AttachTo(this, false);
			}
			Text.DisplayText = "";
			if (Text.Parent == null)
			{
				Text.X = -750f;
			}
			else
			{
				Text.RelativeX = -750f;
			}
			if (Text.Parent == null)
			{
				Text.Y = 100f;
			}
			else
			{
				Text.RelativeY = 100f;
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
			if (Text != null)
			{
				TextManager.RemoveTextOneWay(Text);
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
			Text.DisplayText = "";
			if (Text.Parent == null)
			{
				Text.X = -750f;
			}
			else
			{
				Text.RelativeX = -750f;
			}
			if (Text.Parent == null)
			{
				Text.Y = 100f;
			}
			else
			{
				Text.RelativeY = 100f;
			}
			score = 0;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			healthBarInstance.ConvertToManuallyUpdated();
			TextManager.ConvertToManuallyUpdated(Text);
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(Text);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			healthBarInstance.MoveToLayer(layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(Text);
			}
			TextManager.AddToLayer(Text, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
