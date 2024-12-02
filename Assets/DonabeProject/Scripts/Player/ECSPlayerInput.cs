using Building;
using Citizen;
using DonabeProject.Manager;
using DonabeProject.UI;
using R3;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
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
        
        private ComponentLookup<CitizenBase> CitizenLookup;
        private ComponentLookup<PrefabEntityComponent> PrefabEntityLookup;
        private ComponentLookup<BuildingBase> BuildingLookup;

        protected override void OnCreate()
        {
            var filter = new CollisionFilter()
            {
                BelongsTo = ~0u,
                CollidesWith =  ~0u
            };
            //Debug.Log($"{filter.CollidesWith}");
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
            PrefabEntityLookup = SystemAPI.GetComponentLookup<PrefabEntityComponent>();
            BuildingLookup = SystemAPI.GetComponentLookup<BuildingBase>();
        }

        public void ClickRaycast(Vector3 inputOrigin, Vector3 inputDirection)
        {
            physics = SystemAPI.GetSingleton<PhysicsWorldSingleton>();

            input.Start = inputOrigin;
            
            var distance = 1000;
            var goal = inputDirection * distance;
            input.End = goal + inputOrigin;
            
            if (physics.CastRay(input, out var hit))
            {
                var name = this.EntityManager.GetName(hit.Entity);
                Debug.Log(name);
                
                if (CitizenLookup.HasComponent(hit.Entity))
                {
                    showInfoSubject.OnNext(new CitizenInfoData
                    {
                        pocketMoney = CitizenLookup[hit.Entity].pocketMoney,
                        appetite = CitizenLookup[hit.Entity].appetite
                    });
                }

                if (name == "Plane")
                {
                    foreach (var buffer in SystemAPI.Query<DynamicBuffer<PrefabElement>>().WithAll<PrefabEntityComponent>())
                    {
                        for (int i = 0; i < buffer.Length; i++)
                        {
                            var entity = buffer[i].prefabEntity;
                            if (PlayerStatusHolder.I.NowSelectConstructBuildingID == BuildingLookup[entity].BuildingID)
                            {
                                Debug.Log($"Construct: {PlayerStatusHolder.I.NowSelectConstructBuildingID}");
                                var buildingTransform = SystemAPI.GetComponentRW<LocalTransform>(entity);
                                buildingTransform.ValueRW.Position = new float3(hit.Position.x, hit.Position.y + buildingTransform.ValueRW.Scale/2, hit.Position.z);
                                EntityManager.Instantiate(entity);
                            }
                        }
                    }
                }
            }
        }
    }
}