using DonabeProject.Player;
using UnityEngine;

namespace DonabeProject.Manager
{
    public class PlayerStatusHolder
    {
        public static PlayerStatusHolder I;
        public bool isBlockClickRaycast;
        public NowPlayerMode PlayerMode;
        public int NowSelectConstructBuildingID;
        
        public PlayerStatusHolder()
        {
            I = this;
            isBlockClickRaycast = false;
            PlayerMode = NowPlayerMode.None;
        }
    }
}