namespace TakeCare
{
    using UnityEngine;


    public class Game : MonoBehaviour
    {
        private static Game _instance;


        public void Awake()
        {
            _instance = this;
        }
    }
}