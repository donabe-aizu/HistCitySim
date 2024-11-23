using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

namespace DonabeProject.Player
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    [BurstCompile]
    public partial class ECSPlayerInput : SystemBase
    {
        RaycastInput input;
        PhysicsWorldSingleton physics;
        
        protected override void OnCreate()
        {
            var filter = new CollisionFilter()
            {
                BelongsTo = ~0u,
                CollidesWith =  ~0u
            };
            Debug.Log($"{filter.CollidesWith}");
            input = new RaycastInput
            {
                Start = new float3(0, 0, 0),
                Filter = filter,
                End = new float3(0, 0, 0)
            };
        }

        protected override void OnUpdate()
        {

        }

        public void ClickRaycast(Vector3 inputOrigin, Vector3 inputDirection)
        {
            physics = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            input.Start = inputOrigin;
            
            var distance = 100;
            var goal = inputDirection * distance;
            input.End = goal + inputOrigin;
            
            if (physics.CastRay(input, out var hit))
            {
                var name = this.EntityManager.GetName(hit.Entity);
                Debug.Log(name);
            }
        }
    }
}