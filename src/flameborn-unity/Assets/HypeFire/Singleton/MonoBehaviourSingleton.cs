using HF.Extensions;
using UnityEngine;

namespace HF.Library.Utilities.Singleton
{
	/// <summary>
	/// Singleton niteliği eklenmek istenilen MonoBehaviour yapıları için temel sınıftır.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static bool isExits => _instance.IsNotNull() && _instance.gameObject.IsNotNull();
		protected static T _instance = null;
		protected static T GetInstance() => CreateOrFind();
		private static readonly object padlock = new object();
		[field: SerializeField] public bool UseDontDestroy { get; set; } = true;

		protected virtual void Awake()
		{
			if (GetInstance() == this)
			{
				FindAndDeleteOthers();

				if (UseDontDestroy)
				{
					DontDestroyOnLoad(this.gameObject);
				}
			}
		}

		private static T CreateOrFind()
		{
			if (!isExits)
			{
				var objs = FindObjectsOfType(typeof(T)) as T[];
				if (objs.Length > 0)
				{
					_instance = objs[^1];
					return _instance;
				}
				else
				{
					lock (padlock)
					{
						GameObject go = new GameObject
						{
							name = typeof(T).ToString(),
							hideFlags = HideFlags.DontSave
						};
						_instance = go.AddComponent<T>();
					}
				}
			}

			return _instance;
		}

		private void FindAndDeleteOthers()
		{
			var objs = FindObjectsOfType(typeof(T)) as T[];
			if (objs.Length > 1)
			{
				foreach (var component in objs)
				{
					if (!component.gameObject.Equals(GetInstance().gameObject))
					{
						DestroyImmediate(component.gameObject);
					}
				}
			}
		}

		protected MonoBehaviourSingleton()
		{
		}
	}
}