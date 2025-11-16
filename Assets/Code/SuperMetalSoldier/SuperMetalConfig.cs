using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
    [CreateAssetMenu]
    public class SuperMetalConfig : ScriptableObject
    {
        public float3 PlayerInitialPos;
        public float PlayerWalkAcceleration;
        public float PlayerRunAcceleration;
        public float PlayerWalkMaxHorizontalSpeed;
        public float PlayerRunMaxHorizontalSpeed;
        public float PlayerDeceleration;
        public float PlayerJumpCooldownAfterGrounded;
        public float JumpPushAmount;
        public float GroundedDistanceCheck;
        public float PlayerBodyLength;
        public float StopVelocityLengthThreshold;
        public float PlayerMovementTrailDuration;
        
        public float3 CameraOffset;
        public float CameraLookUpOffset;

        public float Gravity;
    }
}