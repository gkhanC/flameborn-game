using HF.Extensions;
using UnityEngine;

namespace HF.Library.Utilities.Singleton
{
	/// <summary>
	/// Singleton niteliği eklenmek istenilen MonoBehaviour yapıları için temel sınıftır.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class MonoBehaviourSingleton<T> : MonoBehaviour where T : Component
	{
		protected static T _instance = null;
		private static readonly object padlock = new object();
		[field: SerializeField] public bool useDontDestroy { get; set; } = false;
		public static T GetInstance() => CreateOrFind();
		public bool isExits => _instance.IsNotNull() && _instance.gameObject.IsNotNull();

		private static T CreateOrFind()
		{
			if (_instance.IsNull() || _instance.gameObject.IsNull())
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

		public virtual void Awake()
		{
			FindAndDeleteOthers();

			if (useDontDestroy)
			{
				DontDestroyOnLoad(this.gameObject);
			}
		}

		public void SetInstance(T instance) => _instance = instance;

		public void FindAndDeleteOthers()
		{
			var objs = FindObjectsOfType(typeof(T)) as T[];
			if (objs.Length > 1)
			{
				foreach (var component in objs)
				{
					if (!component.gameObject.Equals(_instance.gameObject))
					{
						DestroyImmediate(component);
					}
				}
			}
		}

		protected MonoBehaviourSingleton()
		{
		}
	}
}