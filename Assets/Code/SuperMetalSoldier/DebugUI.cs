using System.Text;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
	public class DebugUI : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI Text;

		private void Update()
		{
			var sb = new StringBuilder();

			sb.AppendLine();

			// Data
			{
				var game = SuperMetalGame.Instance;
				sb.AppendLine($"SpeedXZ: {math.length(game.Player.Velocity.xz)}");
			}

			Text.text = sb.ToString();
		}
	}
}