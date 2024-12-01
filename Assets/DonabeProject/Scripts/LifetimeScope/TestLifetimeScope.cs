using DonabeProject.Player;
using Unity.Entities;
using VContainer;
using VContainer.Unity;

public class TestLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.UseDefaultWorld(systems =>
        {
            systems.Add<SystemA>();
            systems.Add<SystemB>();
        });
        
        builder.RegisterNewWorld("My World 1", Lifetime.Scoped);
        
        builder.RegisterSystemIntoWorld<SystemC>("My World 1");
        
        builder.Register<ServiceA>(Lifetime.Singleton);
    }
}
