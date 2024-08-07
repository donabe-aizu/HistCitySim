﻿using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct PulseSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new PulseUpdateJob()
            { Time = (float)SystemAPI.Time.ElapsedTime };
        job.ScheduleParallel();
    }
}

[BurstCompile]
partial struct PulseUpdateJob : IJobEntity
{
    public float Time;

    void Execute(in CubeTest dancer,
        in Walker walker,
        ref LocalTransform xform)
    {
        var t = dancer.speed * Time;
        xform.Scale = 1.1f - 0.3f * math.abs(math.cos(t));
    }
}