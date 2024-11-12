using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Citizen
{
    public partial struct CitizenSystem : ISystem
    {
        [BurstCompile] // BurstCompilerによる高速化
        public void OnUpdate(ref SystemState state)
        {
            // Jobの発行処理
            var job = new CitizenUpdateJob() 
                { Elapsed = (float)SystemAPI.Time.ElapsedTime }; // 初期化
            job.ScheduleParallel(); // Jobの予約
        }
    }
    
    [BurstCompile] // BurstCompilerによる高速化
    partial struct CitizenUpdateJob : IJobEntity // C#JobSystemによるUpdateの書き換え(partial→SourceGeneratorとの共存)
    {
        public float Elapsed; // 経過時間

        void Execute(in CitizenBase citizen, ref LocalTransform transform) // Queryの代わり。クエリ条件から合致するEntityを探して実行
        {
            Move(citizen, ref transform);
        }

        void Move(CitizenBase citizen, ref LocalTransform transform)
        {
            // 現在位置から目的地に向かって一定のスピードで移動
            transform.Position = Vector3.MoveTowards(transform.Position, citizen.destination, citizen.moveSpeed * Elapsed);
        
            // 目的地に到達したかどうかのチェック
            if (Vector3.Distance(transform.Position, citizen.destination) < 0.01f)
            {
                Debug.Log("目的地に到達しました！");
            }
        }

        void IncreaseDesire(CitizenBase citizen)
        {
            citizen.appetite++;
        }

        void ChangeDestination(CitizenBase citizen)
        {
            
        }
    }
}