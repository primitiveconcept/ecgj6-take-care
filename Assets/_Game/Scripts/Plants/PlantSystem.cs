namespace TakeCare
{
    using UnityEngine;


    public class PlantSystem : MonoBehaviour
    {
        private static PlantSystem instance;


        public void Awake()
        {
            instance = this;
        }
    }
}