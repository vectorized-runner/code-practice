using Unity.Mathematics;
using UnityEngine;

namespace SuperMetalSoldier
{
    [CreateAssetMenu]
    public class SuperMetalConfig : ScriptableObject
    {
        public float3 PlayerInitialPos;
        public float PlayerWalkSpeed;
        public float PlayerRunSpeed;

        public float3 CameraOffset;
        public float CameraLookUpOffset;

        public float Gravity;
    }
}