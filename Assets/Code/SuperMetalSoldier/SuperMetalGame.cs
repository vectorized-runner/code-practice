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

    [Serializable]
    public struct CameraData
    {
        public float3 Position;
        public quaternion Rotation;
    }
    
    public class SuperMetalGame : MonoBehaviour
    {
        public PlayerData Player;
        public CameraData Camera;
        public GameObject PlayerRender;
        public SuperMetalConfig Config;
        public Camera CameraRender;
        
        private void Start()
        {
            // Init player pos
            {
                Player.Position = Config.PlayerInitialPos;
            }
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
                var isRunning = Input.GetKey(KeyCode.LeftShift);
                var moveSpeedMultiplier = isRunning ? Config.PlayerRunSpeed : Config.PlayerWalkSpeed;
                
                if (!allZero)
                {
                    Player.Position += moveInput.x0y() * moveSpeedMultiplier * dt;
                }
                
                Player.Rotation = quaternion.identity;
            }

            // Update Camera
            {
                var playerPos = Player.Position;
                var newCameraPos = playerPos + Config.CameraOffset;
                Camera.Position = newCameraPos;
                var lookDir = playerPos + new float3(0f, Config.CameraLookUpOffset, 0f) - newCameraPos;
                Camera.Rotation = quaternion.LookRotation(lookDir, math.up());
            }

            // Sync Render
            {
                PlayerRender.transform.position = Player.Position;
                PlayerRender.transform.rotation = Player.Rotation;
            }
        }

        private void LateUpdate()
        {
            // Sync Camera
            {
                CameraRender.transform.position = Camera.Position;
                CameraRender.transform.rotation = Camera.Rotation;
            }
        }
    }

}

