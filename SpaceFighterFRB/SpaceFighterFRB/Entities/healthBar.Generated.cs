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
	public partial class healthBar : PositionedObject, IDestroyable
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
			full = 2, 
			empty = 3
		}
		protected int mCurrentState = 0;
		public Entities.healthBar.VariableState CurrentState
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
					case  VariableState.full:
						barScaleX = 22f;
						if (bar.Parent == null)
						{
							barX = 0f;
						}
						else
						{
							bar.RelativeX = 0f;
						}
						break;
					case  VariableState.empty:
						barScaleX = 0f;
						if (bar.Parent == null)
						{
							barX = -22f;
						}
						else
						{
							bar.RelativeX = -22f;
						}
						break;
				}
			}
		}
		static object mLockObject = new object();
		static List<string> mRegisteredUnloads = new List<string>();
		static List<string> LoadedContentManagers = new List<string>();
		
		private FlatRedBall.Sprite frame;
		private FlatRedBall.Sprite bar;
		public float barBlue
		{
			get
			{
				return bar.Blue;
			}
			set
			{
				bar.Blue = value;
			}
		}
		public float barGreen
		{
			get
			{
				return bar.Green;
			}
			set
			{
				bar.Green = value;
			}
		}
		public float barRed
		{
			get
			{
				return bar.Red;
			}
			set
			{
				bar.Red = value;
			}
		}
		public float frameBlue
		{
			get
			{
				return frame.Blue;
			}
			set
			{
				frame.Blue = value;
			}
		}
		public float frameGreen
		{
			get
			{
				return frame.Green;
			}
			set
			{
				frame.Green = value;
			}
		}
		public float frameRed
		{
			get
			{
				return frame.Red;
			}
			set
			{
				frame.Red = value;
			}
		}
		public float barScaleX
		{
			get
			{
				return bar.ScaleX;
			}
			set
			{
				bar.ScaleX = value;
			}
		}
		public float barX
		{
			get
			{
				if (bar.Parent == null)
				{
					return bar.X;
				}
				else
				{
					return bar.RelativeX;
				}
			}
			set
			{
				if (bar.Parent == null)
				{
					bar.X = value;
				}
				else
				{
					bar.RelativeX = value;
				}
			}
		}
		protected Layer LayerProvidedByContainer = null;

        public healthBar()
            : this(FlatRedBall.Screens.ScreenManager.CurrentScreen.ContentManagerName, true)
        {

        }

        public healthBar(string contentManagerName) :
            this(contentManagerName, true)
        {
        }


        public healthBar(string contentManagerName, bool addToManagers) :
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
			frame = new FlatRedBall.Sprite();
			frame.Name = "frame";
			bar = new FlatRedBall.Sprite();
			bar.Name = "bar";
			
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
			SpriteManager.AddToLayer(frame, LayerProvidedByContainer);
			SpriteManager.AddToLayer(bar, LayerProvidedByContainer);
		}
		public virtual void AddToManagers (Layer layerToAddTo)
		{
			LayerProvidedByContainer = layerToAddTo;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(frame, LayerProvidedByContainer);
			SpriteManager.AddToLayer(bar, LayerProvidedByContainer);
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
			
			if (frame != null)
			{
				SpriteManager.RemoveSprite(frame);
			}
			if (bar != null)
			{
				SpriteManager.RemoveSprite(bar);
			}


			CustomDestroy();
		}

		// Generated Methods
		public virtual void PostInitialize ()
		{
			bool oldShapeManagerSuppressAdd = FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = true;
			if (frame.Parent == null)
			{
				frame.CopyAbsoluteToRelative();
				frame.AttachTo(this, false);
			}
			frame.TextureScale = 1f;
			frame.ScaleX = 24f;
			frame.ScaleY = 6f;
			if (bar.Parent == null)
			{
				bar.CopyAbsoluteToRelative();
				bar.AttachTo(this, false);
			}
			bar.TextureScale = 1f;
			if (bar.Parent == null)
			{
				bar.Z = 1f;
			}
			else
			{
				bar.RelativeZ = 1f;
			}
			bar.ScaleX = 22f;
			bar.ScaleY = 4f;
			FlatRedBall.Math.Geometry.ShapeManager.SuppressAddingOnVisibilityTrue = oldShapeManagerSuppressAdd;
		}
		public virtual void AddToManagersBottomUp (Layer layerToAddTo)
		{
			AssignCustomVariables(false);
		}
		public virtual void RemoveFromManagers ()
		{
			SpriteManager.ConvertToManuallyUpdated(this);
			if (frame != null)
			{
				SpriteManager.RemoveSpriteOneWay(frame);
			}
			if (bar != null)
			{
				SpriteManager.RemoveSpriteOneWay(bar);
			}
		}
		public virtual void AssignCustomVariables (bool callOnContainedElements)
		{
			if (callOnContainedElements)
			{
			}
			frame.TextureScale = 1f;
			frame.ScaleX = 24f;
			frame.ScaleY = 6f;
			bar.TextureScale = 1f;
			if (bar.Parent == null)
			{
				bar.Z = 1f;
			}
			else
			{
				bar.RelativeZ = 1f;
			}
			bar.ScaleX = 22f;
			bar.ScaleY = 4f;
			barBlue = 0f;
			barGreen = 1f;
			barRed = 0f;
			frameBlue = 0f;
			frameGreen = 1f;
			frameRed = 1f;
			barScaleX = 22f;
			barX = 0f;
		}
		public virtual void ConvertToManuallyUpdated ()
		{
			this.ForceUpdateDependenciesDeep();
			SpriteManager.ConvertToManuallyUpdated(this);
			SpriteManager.ConvertToManuallyUpdated(frame);
			SpriteManager.ConvertToManuallyUpdated(bar);
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
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("healthBarStaticUnload", UnloadStaticContent);
						mRegisteredUnloads.Add(ContentManagerName);
					}
				}
			}
			if (registerUnload && ContentManagerName != FlatRedBallServices.GlobalContentManager)
			{
				lock (mLockObject)
				{
					if (!mRegisteredUnloads.Contains(ContentManagerName) && ContentManagerName != FlatRedBallServices.GlobalContentManager)
					{
						FlatRedBallServices.GetContentManagerByName(ContentManagerName).AddUnloadMethod("healthBarStaticUnload", UnloadStaticContent);
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
				case  VariableState.full:
					bar.ScaleXVelocity = (22f - bar.ScaleX) / (float)secondsToTake;
					if (bar.Parent != null)
					{
						bar.RelativeXVelocity = (0f - bar.RelativeX) / (float)secondsToTake;
					}
					else
					{
						bar.XVelocity = (0f - bar.X) / (float)secondsToTake;
					}
					break;
				case  VariableState.empty:
					bar.ScaleXVelocity = (0f - bar.ScaleX) / (float)secondsToTake;
					if (bar.Parent != null)
					{
						bar.RelativeXVelocity = (-22f - bar.RelativeX) / (float)secondsToTake;
					}
					else
					{
						bar.XVelocity = (-22f - bar.X) / (float)secondsToTake;
					}
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
				case  VariableState.full:
					bar.ScaleXVelocity =  0;
					if (bar.Parent != null)
					{
						bar.RelativeXVelocity =  0;
					}
					else
					{
						bar.XVelocity =  0;
					}
					break;
				case  VariableState.empty:
					bar.ScaleXVelocity =  0;
					if (bar.Parent != null)
					{
						bar.RelativeXVelocity =  0;
					}
					else
					{
						bar.XVelocity =  0;
					}
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
			bool setbarScaleX = true;
			float barScaleXFirstValue= 0;
			float barScaleXSecondValue= 0;
			bool setbarX = true;
			float barXFirstValue= 0;
			float barXSecondValue= 0;
			switch(firstState)
			{
				case  VariableState.full:
					barScaleXFirstValue = 22f;
					barXFirstValue = 0f;
					break;
				case  VariableState.empty:
					barScaleXFirstValue = 0f;
					barXFirstValue = -22f;
					break;
			}
			switch(secondState)
			{
				case  VariableState.full:
					barScaleXSecondValue = 22f;
					barXSecondValue = 0f;
					break;
				case  VariableState.empty:
					barScaleXSecondValue = 0f;
					barXSecondValue = -22f;
					break;
			}
			if (setbarScaleX)
			{
				barScaleX = barScaleXFirstValue * (1 - interpolationValue) + barScaleXSecondValue * interpolationValue;
			}
			if (setbarX)
			{
				if (bar.Parent != null)
				{
					bar.RelativeX = barXFirstValue * (1 - interpolationValue) + barXSecondValue * interpolationValue;
				}
				else
				{
					barX = barXFirstValue * (1 - interpolationValue) + barXSecondValue * interpolationValue;
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
		}
		public static void PreloadStateContent (VariableState state, string contentManagerName)
		{
			ContentManagerName = contentManagerName;
			switch(state)
			{
				case  VariableState.full:
					break;
				case  VariableState.empty:
					break;
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
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(frame);
			FlatRedBall.Instructions.InstructionManager.IgnorePausingFor(bar);
		}
		public virtual void MoveToLayer (Layer layerToMoveTo)
		{
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(frame);
			}
			SpriteManager.AddToLayer(frame, layerToMoveTo);
			if (LayerProvidedByContainer != null)
			{
				LayerProvidedByContainer.Remove(bar);
			}
			SpriteManager.AddToLayer(bar, layerToMoveTo);
			LayerProvidedByContainer = layerToMoveTo;
		}

    }
	
	
	// Extra classes
	
}
