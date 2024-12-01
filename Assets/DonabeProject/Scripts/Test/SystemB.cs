using Unity.Entities;
using VContainer;

public partial class SystemB : SystemBase
{
    [Inject]
    private SystemA _systemA;
    
    protected override void OnUpdate()
    {
        if (_systemA == null) return;
        _systemA.TestMessage();
    }
}