using Building;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Citizen
{
    public partial struct CitizenMoveSystem : ISystem
    {
        private EntityQuery _buildingQuery;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _buildingQuery = state.GetEntityQuery(typeof(LocalTransform), typeof(BuildingBase));
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // Jobの発行処理
            var job = new CitizenMoveUpdateJob()
            {
                Elapsed = (float)SystemAPI.Time.ElapsedTime,
                BuildingEntities = _buildingQuery.ToEntityArray(Allocator.TempJob),
                BuildingLookup = SystemAPI.GetComponentLookup<BuildingBase>(),
                BuildingPosLookup = SystemAPI.GetComponentLookup<LocalToWorld>()
            }; // 初期化
            job.ScheduleParallel(); // Jobの予約
        }
    }

    [BurstCompile] // BurstCompilerによる高速化
    partial struct CitizenMoveUpdateJob : IJobEntity // C#JobSystemによるUpdateの書き換え(partial→SourceGeneratorとの共存)
    {
        public float Elapsed; // 経過時間
        public NativeArray<Entity> BuildingEntities;
        [ReadOnly] public ComponentLookup<BuildingBase> BuildingLookup;
        [ReadOnly] public ComponentLookup<LocalToWorld> BuildingPosLookup;
        
        private Entity closestBuildingEntity;

        void Execute(ref CitizenBase citizen, ref LocalTransform transform) // Queryの代わり。クエリ条件から合致するEntityを探して実行
        {
            float minDistanceSq = float.MaxValue;

            citizen.IncreaseRandomDesire();

            if (!citizen.isNowMove)
            {
                if (IsAlreadyArrived(transform.Position,citizen.home))
                {
                    if (citizen.appetite<500 || citizen.pocketMoney<500)
                    {
                        foreach (var buildingEntity in BuildingEntities)
                        {
                            if (BuildingLookup[buildingEntity].buildingType == BuildingType.Office)
                            {
                                float distanceSq = math.distancesq(BuildingPosLookup[buildingEntity].Position, transform.Position);

                                if (distanceSq < minDistanceSq)
                                {
                                    minDistanceSq = distanceSq;
                                    closestBuildingEntity = buildingEntity;
                                    citizen.destinationEntity = closestBuildingEntity;
                                }
                            }
                        }
                        citizen.destination = BuildingPosLookup[closestBuildingEntity].Position;
                    }
                    else
                    {
                        foreach (var buildingEntity in BuildingEntities)
                        {
                            if (BuildingLookup[buildingEntity].buildingType == BuildingType.Food)
                            {
                                float distanceSq = math.distancesq(BuildingPosLookup[buildingEntity].Position, transform.Position);

                                if (distanceSq < minDistanceSq)
                                {
                                    minDistanceSq = distanceSq;
                                    closestBuildingEntity = buildingEntity;
                                    citizen.destinationEntity = closestBuildingEntity;
                                }
                            }
                        }

                        citizen.destination = BuildingPosLookup[closestBuildingEntity].Position;
                    }
                }
                else if (IsAlreadyArrived(transform.Position,citizen.destination))
                {
                    if (citizen.destinationEntity==Entity.Null)
                    {
                        citizen.destination = citizen.home;
                    }
                    else
                    {
                        if (BuildingLookup[citizen.destinationEntity].buildingType == BuildingType.Food)
                        {
                            citizen.destination = citizen.home;
                            citizen.appetite = 0;
                            citizen.pocketMoney -= 500;
                        }
                        else if (BuildingLookup[citizen.destinationEntity].buildingType == BuildingType.Office)
                        {
                            citizen.destination = citizen.home;
                            citizen.pocketMoney += 500;
                        }
                    }
                }
            }
            
            Move(ref citizen, ref transform);
        }

        void Move(ref CitizenBase citizen, ref LocalTransform transform)
        {
            transform.Position = Vector3.MoveTowards(transform.Position, citizen.destination, citizen.moveSpeed * Elapsed);

            citizen.isNowMove = !IsAlreadyArrived(transform.Position,citizen.destination);
        }

        bool IsAlreadyArrived(Vector3 citizenPos, Vector3 destination)
        {
            // 目的地に到達したかどうかのチェック
            if (Vector3.Distance(citizenPos, destination) < 0.00001f)
            {
                //Debug.Log("目的地に到達しました！");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}