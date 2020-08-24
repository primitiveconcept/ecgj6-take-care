namespace TakeCare
{
    using UnityEngine;
    using UnityEngine.Serialization;


    public class Game : MonoBehaviour
    {
        private static Game instance;

        [SerializeField]
        private Sprite[] leafSprites;
        [SerializeField]
        private Sprite[] branchSprites;
        [SerializeField]
        private Sprite[] featureSprites;


        public static Sprite[] LeafSprites
        {
            get { return instance.leafSprites; }
        }


        public static Sprite[] BranchSprites
        {
            get { return instance.branchSprites; }
        }


        public static Sprite[] FeatureSprites
        {
            get { return instance.featureSprites; }
        }


        public void Awake()
        {
            instance = this;
        }
    }
}