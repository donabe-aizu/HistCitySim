using Unity.Burst;
using Unity.Entities;

namespace Building
{
    public partial struct  BuildingSystem : ISystem
    {
        [BurstCompile] // BurstCompilerによる高速化
        public void OnUpdate(ref SystemState state)
        {
            // Jobの発行処理
            var job = new BuildingUpdateJob() 
                { Elapsed = (float)SystemAPI.Time.ElapsedTime }; // 初期化
            job.ScheduleParallel(); // Jobの予約
        }
    }
    
    [BurstCompile] // BurstCompilerによる高速化
    partial struct BuildingUpdateJob : IJobEntity
    {
        public float Elapsed; // 経過時間

        void Execute(in BuildingBase building)
        {
            
        }
    }
}