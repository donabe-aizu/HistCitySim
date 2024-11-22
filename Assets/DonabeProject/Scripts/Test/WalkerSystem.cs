using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct WalkerSystem : ISystem
{
    [BurstCompile] // BurstCompilerによる高速化
    public void OnUpdate(ref SystemState state)
    {
        // Jobの発行処理
        var job = new WalkerUpdateJob() 
            { DeltaTime = (float)SystemAPI.Time.DeltaTime }; // 初期化
        job.ScheduleParallel(); // Jobの予約
    }
}

[BurstCompile] // BurstCompilerによる高速化
partial struct WalkerUpdateJob : IJobEntity
{
    public float DeltaTime;

    void Execute(in Walker walker, ref LocalTransform xform)
    {
        var rot = quaternion.RotateY(walker.AngularSpeed * DeltaTime);
        var fwd = math.mul(rot, xform.Forward());
        xform.Position += fwd * walker.ForwardSpeed * DeltaTime;
        xform.Rotation = quaternion.LookRotation(fwd, xform.Up());
    }
}