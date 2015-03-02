using SpaceFighterFRB.Entities;
using System;
using FlatRedBall.Math;
using FlatRedBall.Graphics;
using SpaceFighterFRB.Performance;

namespace SpaceFighterFRB.Factories
{
	public class enemyShipFactory : IEntityFactory
	{
		public static enemyShip CreateNew ()
		{
			return CreateNew(null);
		}
		public static enemyShip CreateNew (Layer layer)
		{
			if (string.IsNullOrEmpty(mContentManagerName))
			{
				throw new System.Exception("You must first initialize the factory to use it.");
			}
			enemyShip instance = null;
			instance = new enemyShip(mContentManagerName, false);
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
		
		public static void Initialize (PositionedObjectList<enemyShip> listFromScreen, string contentManager)
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
				enemyShip instance = new enemyShip(mContentManagerName, false);
				mPool.AddToPool(instance);
			}
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (enemyShip objectToMakeUnused)
		{
			MakeUnused(objectToMakeUnused, true);
		}
		
		/// <summary>
		/// Makes the argument objectToMakeUnused marked as unused.  This method is generated to be used
		/// by generated code.  Use Destroy instead when writing custom code so that your code will behave
		/// the same whether your Entity is pooled or not.
		/// </summary>
		public static void MakeUnused (enemyShip objectToMakeUnused, bool callDestroy)
		{
			objectToMakeUnused.Destroy();
		}
		
		
			static string mContentManagerName;
			static PositionedObjectList<enemyShip> mScreenListReference;
			static PoolList<enemyShip> mPool = new PoolList<enemyShip>();
			public static Action<enemyShip> EntitySpawned;
			object IEntityFactory.CreateNew ()
			{
				return enemyShipFactory.CreateNew();
			}
			object IEntityFactory.CreateNew (Layer layer)
			{
				return enemyShipFactory.CreateNew(layer);
			}
			public static PositionedObjectList<enemyShip> ScreenListReference
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
			static enemyShipFactory mSelf;
			public static enemyShipFactory Self
			{
				get
				{
					if (mSelf == null)
					{
						mSelf = new enemyShipFactory();
					}
					return mSelf;
				}
			}
	}
}
