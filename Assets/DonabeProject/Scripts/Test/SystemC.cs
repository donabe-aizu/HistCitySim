using Unity.Entities;

public partial class SystemC : SystemBase
{
    readonly ServiceA serviceA;
    
    // Constructor injection
    public SystemC(ServiceA serviceA)
    {
        this.serviceA = serviceA;
    }
    
    protected override void OnUpdate()
    {
        serviceA.TestMessage();
    }
}