using DonabeProject.Player;
using UnityEngine;

namespace DonabeProject.Manager
{
    public class PlayerStatusHolder
    {
        public static PlayerStatusHolder I;
        public bool isBlockClickRaycast;
        public NowPlayerMode PlayerMode;
        public GameObject NowSelectConstructBuilding;
        
        public PlayerStatusHolder()
        {
            I = this;
            isBlockClickRaycast = false;
            PlayerMode = NowPlayerMode.None;
        }
    }
}