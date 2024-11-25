using DonabeProject.Player;
using DonabeProject.UI;
using R3;
using Unity.Entities;
using UnityEngine;

namespace DonabeProject.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] 
        private SelectEntityInfoView selectEntityInfoView;
        private ECSPlayerInput ecsPlayerInput;
        
        void Start()
        {
            var world = World.DefaultGameObjectInjectionWorld;
            ecsPlayerInput = world.GetExistingSystemManaged<ECSPlayerInput>();

            ecsPlayerInput.onShowInfo.Subscribe(click =>
            {
                selectEntityInfoView.ChangeView(click);
            }).AddTo(this);
        }
    }
}