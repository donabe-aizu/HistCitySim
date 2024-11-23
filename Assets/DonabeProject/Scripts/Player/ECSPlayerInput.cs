using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace DonabeProject.Player
{
    public partial class ECSPlayerInput : SystemBase
    {
        public Vector3 inputOrigin;
        public Vector3 inputDirection;
        
        RaycastInput input;
        PhysicsWorldSingleton physics;

        protected override void OnUpdate()
        {

        }
    }
}