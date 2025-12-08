using System;
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
        public float PlayerTurnAnglePerSec;
        public float PlayerRotationSmoothTime;
        
        public float3 CameraOffset;
        public float CameraLookUpOffset;

        public float Gravity;

        public GameObject EnemyPrefab;

        private static SuperMetalConfig _instance;

        public static SuperMetalConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<SuperMetalConfig>("SuperMetalConfig");
                    if (_instance == null)
                    {
                        throw new Exception("Couldn't load the SuperMetalConfig instance.");
                    }
                }

                return _instance;
            }
        }
    }
}