using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.Splines;

[UpdateInGroup(typeof(InitializationSystemGroup))] // Updateのタイミングを明示的に
public partial struct RoadSystem : ISystem
{
    [BurstCompile] // BurstCompilerによる高速化
    public void OnUpdate(ref SystemState state)
    {
        // Jobの発行処理
        var job = new RoadUpdateJob() 
            { Elapsed = (float)SystemAPI.Time.ElapsedTime }; // 初期化
        job.ScheduleParallel(); // Jobの予約
    }
}

[BurstCompile] // BurstCompilerによる高速化
partial struct RoadUpdateJob : IJobEntity // C#JobSystemによるUpdateの書き換え(partial→SourceGeneratorとの共存)
{
    public float Elapsed; // 経過時間

    void Execute(in Road road, ref SplineContainer spline) // Queryの代わり。クエリ条件から合致するEntityを探して実行
    {
        
    }
}