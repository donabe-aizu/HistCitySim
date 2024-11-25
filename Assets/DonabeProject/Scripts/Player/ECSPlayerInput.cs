using Citizen;
using DonabeProject.UI;
using R3;
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
        public Observable<CitizenInfoData> onShowInfo => showInfoSubject;
        private Subject<CitizenInfoData> showInfoSubject = new Subject<CitizenInfoData>();
        
        RaycastInput input;
        PhysicsWorldSingleton physics;
        
        ComponentLookup<CitizenBase> CitizenLookup;
        
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
            CitizenLookup = SystemAPI.GetComponentLookup<CitizenBase>();
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
                showInfoSubject.OnNext(new CitizenInfoData
                {
                    pocketMoney = CitizenLookup[hit.Entity].pocketMoney,
                    appetite = CitizenLookup[hit.Entity].appetite
                });
                Debug.Log(name);
            }
        }
    }
}