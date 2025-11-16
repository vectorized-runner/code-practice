using System;
using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
    public static class Extensions
    {
        // ReSharper disable once InconsistentNaming
        public static float3 x0y(this float2 f)
        {
            return new float3(f.x, 0f, f.y);
        }
    }
    
    [Serializable]
    public struct PlayerData
    {
        public float3 Position;
        public quaternion Rotation;
    }
    
    public class SuperMetalGame : MonoBehaviour
    {
        public PlayerData Player;
        public GameObject PlayerRender;
        public SuperMetalConfig Config;
        
        private void Start()
        {
            
        }

        private float2 GetPlayerMoveInput()
        {
            var result = float2.zero;
            
            if (Input.GetKey(KeyCode.W))
            {
                result += new float2(0, 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                result += new float2(0, -1);
            }

            if (Input.GetKey(KeyCode.A))
            {
                result += new float2(-1, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                result += new float2(1, 0);
            }

            return result;
        }


        private void Update()
        {
            var dt = Time.deltaTime;
            
            // Update Player Pos
            {
                var moveInput = GetPlayerMoveInput();
                var allZero = math.all(moveInput == float2.zero);
                
                if (!allZero)
                {
                    Player.Position += moveInput.x0y() * Config.PlayerMoveSpeed * dt;
                }
                
                Player.Rotation = quaternion.identity;
            }


            // Sync Render
            {
                PlayerRender.transform.position = Player.Position;
                PlayerRender.transform.rotation = Player.Rotation;
            }
        }
    }

}

