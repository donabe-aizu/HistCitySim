namespace DonabeProject.Manager
{
    public class PlayerStatusHolder
    {
        public static PlayerStatusHolder I;
        public bool isBlockClickRaycast;
        
        public PlayerStatusHolder()
        {
            I = this;
            isBlockClickRaycast = false;
        }
    }
}