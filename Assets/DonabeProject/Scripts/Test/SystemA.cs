using Unity.Entities;
using UnityEngine;

public partial class SystemA : SystemBase
{
    protected override void OnUpdate()
    {
        
    }

    public void TestMessage()
    {
        Debug.Log("SystemA");
    }
}