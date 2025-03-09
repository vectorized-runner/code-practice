using System;
using Unity.Collections;
using UnityEngine;

namespace CodePractice
{
    public struct EntityManager
    {
        private int _lastUsedVersion;

        public static EntityManager Create()
        {
            return new EntityManager
            {

            };
        }
        
        public Entity CreateEntity()
        {
            throw new NotImplementedException();
        }

        public void DestroyEntity(Entity entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}