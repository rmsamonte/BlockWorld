// -----------------------------------------------------------------------
//  <copyright Service.cs company="Kahuna Studios">
//      Copyright (c) 2016 Kahuna Studios. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Game.Scripts
{
	/// <summary>
	/// Wrapper template class for services, for optimal setting and getting.
	/// </summary>
	public static class Service
	{
		private static List<IServiceWrapper> serviceWrapperList;

		public static T Set<T>(T instance)
		{
			if (ServiceWrapper<T>.instance != null)
			{
				throw new InvalidOperationException("An instance of this service class has already been set!");
			}

			ServiceWrapper<T>.instance = instance;

			if (serviceWrapperList == null)
			{
				serviceWrapperList = new List<IServiceWrapper>();
			}

			serviceWrapperList.Add(new ServiceWrapper<T>());

			return instance;
		}

		public static T Get<T>()
		{
			return ServiceWrapper<T>.instance;
		}

		public static bool IsSet<T>()
		{
			return ServiceWrapper<T>.instance != null;
		}

		public static void Clear<T>()
		{
			ServiceWrapper<T>.instance = default(T);
		}

		// Resets references to all services back to null so that they can go out of scope and
		// be subjected to garbage collection.
		// * Services that reference each other will be garbage collected.
		// * AssetBundles should be manually unloaded by an asset manager.
		// * GameObjects will be destroyed by the next level load done by the caller.
		// * Any application statics should be reset by the caller as well.
		// * If there are any unmanaged objects, those need to be released by the caller, too.
		public static void ResetAll()
		{
			if (serviceWrapperList == null)
			{
				serviceWrapperList = new List<IServiceWrapper>();
			}

			// Unset in the reverse order in which services were set.  Probably doesn't matter.
			for (int i = serviceWrapperList.Count - 1; i >= 0; i--)
			{
				serviceWrapperList[i].Unset();
			}

			serviceWrapperList.Clear();
		}
	}

	internal class ServiceWrapper<T> : IServiceWrapper
	{
		public static T instance = default(T);

		public void Unset()
		{
			var disposeable = ServiceWrapper<T>.instance as IDisposable;
			if (disposeable != null)
			{
				disposeable.Dispose();
			}
			ServiceWrapper<T>.instance = default(T);
		}
	}

	internal interface IServiceWrapper
	{
		void Unset();
	}
}