using Building;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace Citizen
{
    public partial struct CitizenCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<CitizenBase>();
            state.RequireForUpdate<BuildingBase>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {

        }
    }

    [BurstCompile]
    partial struct CollisionJob : ICollisionEventsJob
    {
        private ComponentLookup<BuildingBase> buildingLookup;

        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;
        }
    }
}