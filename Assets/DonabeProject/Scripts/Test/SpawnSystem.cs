using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))] // Updateのタイミングを明示的に
public partial struct SpawnSystem : ISystem
{
    public void OnCreate(ref SystemState state)
        => state.RequireForUpdate<Config>(); // Configコンポーネントが存在するときだけ実行(非同期的に実行されるので確実にあるか確認)

    [BurstCompile] // BurstCompilerによる高速化
    public void OnUpdate(ref SystemState state)
    {
        var config = SystemAPI.GetSingleton<Config>(); // シングルトンのConfig取得

        var instances = state.EntityManager.Instantiate
            (config.Prefab, config.SpawnCount, Allocator.Temp); // Prefabのインスタンス化、まとめて一回で生成のほうが速い

        var rand = new Random(config.RandomSeed);
        foreach (var entity in instances)
        {
            // 各コンポーネントへのアクセサ取得
            var xform = SystemAPI.GetComponentRW<LocalTransform>(entity);
            var dancer = SystemAPI.GetComponentRW<CubeTest>(entity);
            var walker = SystemAPI.GetComponentRW<Walker>(entity);

            // RandomなTransform
            xform.ValueRW = LocalTransform.FromPositionRotation
                (rand.NextOnDisk() * config.SpawnRadius, rand.NextYRotation());

            // Randomなパラメーター
            dancer.ValueRW = CubeTest.Random(rand.NextUInt());
            walker.ValueRW = Walker.Random(rand.NextUInt());
        }

        // このシステムを停止
        state.Enabled = false;
    }
}