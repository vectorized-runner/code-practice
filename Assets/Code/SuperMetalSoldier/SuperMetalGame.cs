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

    public enum AnimationState
    {
        Idle,
        Walk,
        Run,
    }

    [Serializable]
    public struct PlayerData
    {
        public float3 Position;
        public float3 Velocity;
        public quaternion Rotation;
        public float JumpDuration;
        public float JumpLandedTime;
        public bool IsGrounded;
        public bool IsApplyingGravity;
        public float LastGroundedTime;
        public AnimationState AnimationState;
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
        private Animator _playerAnimator;

        private void Start()
        {
            // Init player pos
            {
                Player.Position = Config.PlayerInitialPos;
                Player.AnimationState = AnimationState.Idle;
            }

            _playerRb = PlayerRender.GetComponent<Rigidbody>();
            _playerAnimator = PlayerRender.GetComponentInChildren<Animator>();
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
            var time = Time.time;

            // Sync back from physics engine
            {
                Player.Position = _playerRb.position;
                Player.Rotation = _playerRb.rotation;
                Player.Velocity = _playerRb.linearVelocity;
            }

            // This is to track movement over time
            Debug.DrawRay(Player.Position, -math.up(), Color.red, Config.PlayerMovementTrailDuration);

            // IsGrounded logic
            {
                const float upOffset = 0.1f;
                var raycastPos = Player.Position + math.up() * (upOffset - Config.PlayerBodyLength);
                var isGrounded = Physics.Raycast(raycastPos, -math.up(), Config.GroundedDistanceCheck, -1,
                    QueryTriggerInteraction.Ignore);

                Debug.DrawRay(raycastPos, -math.up() * Config.GroundedDistanceCheck, Color.yellow, 0.1f);

                if (!Player.IsGrounded && isGrounded)
                {
                    Player.LastGroundedTime = time;
                }

                Player.IsGrounded = isGrounded;
            }

            // Jump
            {
                var isJumpOutOfCooldown = time - Player.LastGroundedTime > Config.PlayerJumpCooldownAfterGrounded;
                var canJump = Player.IsGrounded && isJumpOutOfCooldown;
                if (canJump && Input.GetKeyDown(KeyCode.Space))
                {
                    Player.Velocity += math.up() * Config.JumpPushAmount;
                }
            }

            // Gravity
            {
                if (!Player.IsGrounded)
                {
                    Player.Velocity += -math.up() * Config.Gravity * dt;
                    Player.IsApplyingGravity = true;
                }
                else
                {
                    Player.IsApplyingGravity = false;
                }
            }

            var runInput = Input.GetKey(KeyCode.LeftShift);

            // Update Player Pos
            {
                var moveInput = GetPlayerMoveInput();
                var allZero = math.all(moveInput == float2.zero);
                var accelerationConstant = runInput ? Config.PlayerRunAcceleration : Config.PlayerWalkAcceleration;
                var acceleration = math.normalize(moveInput) * accelerationConstant;

                if (!allZero)
                {
                    var desiredVelocity = Player.Velocity.xz + acceleration * dt;
                    var len = math.length(desiredVelocity);
                    var maxSpeed = runInput ? Config.PlayerRunMaxHorizontalSpeed : Config.PlayerWalkMaxHorizontalSpeed;
                    if (len >= maxSpeed)
                    {
                        var normalized = desiredVelocity / len;
                        desiredVelocity = normalized * maxSpeed;
                    }

                    Player.Velocity.xz = desiredVelocity;


                    // Turn
                    var lookPosition = Player.Position + moveInput.x0y();
                    // var lookDir = lookPosition - Player.Position
                    var wantedRotation = quaternion.LookRotation(moveInput.x0y(), math.up());
                    var rotateDegrees = Config.PlayerTurnAnglePerSec * dt;
                    var rotation = Quaternion.RotateTowards(Player.Rotation, wantedRotation, rotateDegrees);
                    Debug.DrawRay(lookPosition, math.up(), Color.cyan, 10f);
                    Player.Rotation = rotation;

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
            }

            // Update anim
            {
                var previousState = Player.AnimationState;

                if (runInput)
                {
                    Player.AnimationState = AnimationState.Run;
                }
                else
                {
                    // Check velocity
                    const float walkVelocityThreshold = 0.1f;
                    var horizontalVelocity = math.length(Player.Velocity.xz);
                    if (horizontalVelocity < walkVelocityThreshold)
                    {
                        Player.AnimationState = AnimationState.Idle;
                    }
                    else
                    {
                        Player.AnimationState = AnimationState.Walk;
                    }
                }

                // Sync to Animator
                var newState = Player.AnimationState;
                if (previousState != newState)
                {
                    _playerAnimator.SetTrigger(AnimationStateToStr(newState));
                }
            }

            // Sync to Physics
            {
                _playerRb.linearVelocity = Player.Velocity;
                _playerRb.rotation = Player.Rotation;
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

        private string AnimationStateToStr(AnimationState state)
        {
            switch (state)
            {
                case AnimationState.Idle:
                    return "Idle";
                case AnimationState.Walk:
                    return "Walk";
                case AnimationState.Run:
                    return "Run";
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
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