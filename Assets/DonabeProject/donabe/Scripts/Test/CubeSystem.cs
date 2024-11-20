using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct CubeSystem : ISystem
{
    [BurstCompile] // BurstCompilerによる高速化
    public void OnUpdate(ref SystemState state)
    {
        // Jobの発行処理
        var job = new DancerUpdateJob() 
            { Elapsed = (float)SystemAPI.Time.ElapsedTime }; // 初期化
        job.ScheduleParallel(); // Jobの予約
    }
}

[BurstCompile] // BurstCompilerによる高速化
partial struct DancerUpdateJob : IJobEntity // C#JobSystemによるUpdateの書き換え(partial→SourceGeneratorとの共存)
{
    public float Elapsed; // 経過時間

    void Execute(in CubeTest dancer, ref LocalTransform xform) // Queryの代わり。クエリ条件から合致するEntityを探して実行
    {
        var t = dancer.speed * Elapsed;
        var y = math.abs(math.sin(t)) * 0.1f;
        var bank = math.cos(t) * 0.5f;

        var fwd = xform.Forward();
        var rot = quaternion.AxisAngle(fwd, bank);
        var up = math.mul(rot, math.float3(0, 1, 0));

        xform.Position.y = y;
        xform.Rotation = quaternion.LookRotation(fwd, up);
    }
}