using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
	public class CameraController : MonoBehaviour
	{
		public CameraData Camera;
		public Camera CameraRender;

		private void LateUpdate()
		{
			var player = SuperMetalGame.Instance.Player;
			var config = SuperMetalConfig.Instance;
			var playerPos = player.Position;
			var newCameraPos = playerPos + config.CameraOffset;
			Camera.Position = newCameraPos;
			var lookDir = playerPos + new float3(0f, config.CameraLookUpOffset, 0f) - newCameraPos;
			Camera.Rotation = quaternion.LookRotation(lookDir, math.up());
			
			// Sync
			CameraRender.transform.position = Camera.Position;
			CameraRender.transform.rotation = Camera.Rotation;
		}
	}
}