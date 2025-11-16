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
        public float3 Velocity;
        public quaternion Rotation;
        public float JumpDuration;
        public bool IsGrounded;
        public bool IsApplyingGravity;
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
        private Rigidbody _playerRb;
        
        private void Start()
        {
            // Init player pos
            {
                Player.Position = Config.PlayerInitialPos;
            }

            _playerRb = PlayerRender.GetComponent<Rigidbody>();
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

            // Sync back from physics engine
            {
                Player.Position = _playerRb.position;
                Player.Velocity = _playerRb.linearVelocity;
            }
            
            const float upOffset = 0.1f;
            var raycastPos = Player.Position + math.up() * (upOffset - Config.PlayerBodyLength);
            var isGrounded = Physics.Raycast(raycastPos, -math.up(), Config.GroundedDistanceCheck, -1,
                QueryTriggerInteraction.Ignore);
            
            Debug.DrawRay(raycastPos, -math.up() * Config.GroundedDistanceCheck, Color.yellow, 0.1f);

            // This is to track movement over time
            Debug.DrawRay(Player.Position, -math.up(), Color.red, Config.PlayerMovementTrailDuration);
            
            Player.IsGrounded = isGrounded;

            // Gravity
            {
                if (!isGrounded)
                {
                    Player.Velocity += -math.up() * Config.Gravity * dt;
                    Player.IsApplyingGravity = true;
                }
                else
                {
                    Player.IsApplyingGravity = false;
                }
            }
            
            // Update Player Pos
            {
                var moveInput = GetPlayerMoveInput();
                var allZero = math.all(moveInput == float2.zero);
                var isRunning = Input.GetKey(KeyCode.LeftShift);
                var accelerationConstant = isRunning ? Config.PlayerRunAcceleration : Config.PlayerWalkAcceleration;
                var acceleration = math.normalize(moveInput) * accelerationConstant;
                
                if (!allZero)
                {
                    var targetVelocity = Player.Velocity.xz * acceleration * dt;
                    Player.Velocity.xz += targetVelocity;
                    
                    // We don't determine the position, the physics engine does
                    // Player.Position += moveInput.x0y() * moveSpeedMultiplier * dt;
                }
                else
                {
                    Debug.Assert(Config.StopVelocityLengthThreshold > 0.001f);
                    var len = math.length(Player.Velocity.xz);
                    
                    if (len < Config.StopVelocityLengthThreshold)
                    {
                        Player.Velocity.xz = float2.zero;
                    }
                    else
                    {
                        // Decelerate
                        var decelerationDir = -math.normalize(Player.Velocity.xz);
                        var velocityXZ = Player.Velocity.xz;
                        velocityXZ += decelerationDir * dt * Config.PlayerDeceleration;
                        Player.Velocity.xz = velocityXZ;
                    }
                }
                
                Player.Rotation = quaternion.identity;
            }
            
            // Sync to Physics
            {
                _playerRb.linearVelocity = Player.Velocity;
            }

            // Update Camera
            {
                var playerPos = Player.Position;
                var newCameraPos = playerPos + Config.CameraOffset;
                Camera.Position = newCameraPos;
                var lookDir = playerPos + new float3(0f, Config.CameraLookUpOffset, 0f) - newCameraPos;
                Camera.Rotation = quaternion.LookRotation(lookDir, math.up());
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

