using System;
using System.Linq;
using System.Threading.Tasks;
using Building;
using Cysharp.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Physics;

namespace Citizen
{
    public partial struct CitizenSystem : ISystem
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
            // // Jobの発行処理
            // var job = new CitizenUpdateJob()
            // {
            //     Elapsed = (float)SystemAPI.Time.ElapsedTime,
            //     buildingsTransforms = _buildingQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob)
            // }; // 初期化
            // job.ScheduleParallel(); // Jobの予約
        }
    }

    [BurstCompile] // BurstCompilerによる高速化
    partial struct CitizenUpdateJob : IJobEntity // C#JobSystemによるUpdateの書き換え(partial→SourceGeneratorとの共存)
    {
        public float Elapsed; // 経過時間
        public NativeArray<LocalTransform> buildingsTransforms;
        //public PhysicsWorld physicsWorld;
        private bool _isNowDestination;

        void Execute(ref CitizenBase citizen, ref LocalTransform transform) // Queryの代わり。クエリ条件から合致するEntityを探して実行
        {
            LocalTransform closestBuildingTransform = default;
            float minDistanceSq = float.MaxValue;

            citizen.IncreaseRandomDesire();

            if (!citizen.isNowMove)
            {
                if (IsAlreadyArrived(transform.Position,citizen.home))
                {
                    if (citizen.appetite<1000)
                    {
                        return;
                    }
                
                    // 各ビルディングの位置とプレイヤーの位置を比較して最も近いものを探す
                    foreach (var buildingTransform in buildingsTransforms)
                    {
                        float distanceSq = math.distancesq(buildingTransform.Position, transform.Position);

                        if (distanceSq < minDistanceSq)
                        {
                            minDistanceSq = distanceSq;
                            closestBuildingTransform = buildingTransform;
                        }
                    }

                    citizen.destination = closestBuildingTransform.Position;
                }
                else if (IsAlreadyArrived(transform.Position,citizen.destination))
                {
                    citizen.destination = citizen.home;
                    citizen.appetite = 0;
                }
            }
            
            Move(ref citizen, ref transform);
        }

        void Move(ref CitizenBase citizen, ref LocalTransform transform)
        {
            transform.Position = Vector3.MoveTowards(transform.Position, citizen.destination, citizen.moveSpeed * Elapsed);
        
            _isNowDestination = IsAlreadyArrived(transform.Position,citizen.destination);
            
            citizen.isNowMove = !_isNowDestination;
        }
        
        public static DistanceHit GetClosestHit(NativeList<DistanceHit> hits, float3 userPosition)
        {
            DistanceHit closestHit = default;
            float closestDistance = float.MaxValue;

            foreach (var hit in hits)
            {
                float distance = math.distance(userPosition, hit.Position); // 距離計算
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHit = hit;
                }
            }

            return closestHit;
        }

        bool IsAlreadyArrived(Vector3 citizenPos, Vector3 destination)
        {
            // 目的地に到達したかどうかのチェック
            if (Vector3.Distance(citizenPos, destination) < 0.00001f)
            {
                Debug.Log("目的地に到達しました！");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}