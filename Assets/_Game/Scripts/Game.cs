namespace TakeCare
{
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;
    using UnityEngine.UI;


    public class Game : MonoBehaviour
    {
        private static Game instance;

        [SerializeField]
        private Text caption;
        
        [SerializeField]
        private Sprite[] leafSprites;
        [SerializeField]
        private Sprite[] branchSprites;
        [SerializeField]
        private Sprite[] featureSprites;

        [SerializeField]
        private ItemData[] Items;
        
        private Interactable currentInteractable;


        public static void SetInteractable(Interactable interactable)
        {
            instance.currentInteractable = interactable;
        }
        
        public static void UnsetInteractable(Interactable interactable)
        {
            if (!instance.currentInteractable == interactable)
                return;

            instance.currentInteractable = null;
        }

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


        public static Text Caption
        {
            get { return instance.caption; }
        }


        public void Awake()
        {
            instance = this;
        }
    }
}