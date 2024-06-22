using UnityEngine;

namespace HypeFire.UI
{
	public class BilboardUI : MonoBehaviour
	{
		private Camera mainCamera;
		
		private void Start()
		{
			mainCamera = Camera.main;
		}

		private void LateUpdate()
		{
			
			
			transform.rotation = mainCamera.transform.rotation;
		}
	}
}