using SpaceFighterFRB.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using SpaceFighterFRB.Performance;

namespace SpaceFighterFRB.Factories
{
	public class bulletFactory : IEntityFactory
	{
		public static bullet CreateNew ()
		{
			return CreateNew(null);
		}
		public static bullet CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			bullet instance = null;
			instance = new bullet(mContentManagerName, false);
			instance.AddToManagers(layer);
			if (mScreenListReference != null)
			{
				mScreenListReference.Add(instance);
			}
			if (EntitySpawned != null)
			{
				EntitySpawned(instance);
			}
			return instance;
		}
		
		public static void Initialize (PositionedObjectList<bullet> listFromScreen, string contentManager)
		{
			mContentManagerName = contentManager;
			mScreenListReference = listFromScreen;
		}
		
		public static void Destroy ()
		{
			mContentManagerName = null;
			mScreenListReference = null;
			mPool.Clear();
			EntitySpawned = null;
		}
		
		private static void FactoryInitialize ()
		{
			const int numberToPreAllocate = 20;
			for (int i = 0; i < numberToPreAllocate; i++)
			{
				bullet instance = new bullet(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (bullet objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (bullet objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<bullet> mScreenListReference;
			static PoolList<bullet> mPool = new PoolList<bullet>();
			public static Action<bullet> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return bulletFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return bulletFactory.CreateNew(layer);
			}
			public static PositionedObjectList<bullet> ScreenListReference
			{
				get
				{
					return mScreenListReference;
				}
				set
				{
					mScreenListReference = value;
				}
			}
			static bulletFactory mSelf;
			public static bulletFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new bulletFactory();
					}
					return mSelf;
				}
			}
	}
}
