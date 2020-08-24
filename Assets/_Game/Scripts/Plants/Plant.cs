namespace TakeCare
{
    using UnityEditor;
    using UnityEngine;
    

    public class Plant : MonoBehaviour
    {
        public Sprite TileSprite;
        
        [Header("GameObjects")]
        public GameObject Base;
        public GameObject Leaves;
        public GameObject Feature;

        [Header("Data")]
        public PlantResources Resources;

        private SpriteRenderer baseRenderer;
        private SpriteRenderer leavesRenderer;
        private SpriteRenderer featureRenderer;


        public void Awake()
        {
            this.baseRenderer = this.Base.GetComponent<SpriteRenderer>();
            this.leavesRenderer = this.Base.GetComponent<SpriteRenderer>();
            this.featureRenderer = this.Base.GetComponent<SpriteRenderer>();
        }

        public void Start()
        {
            Fractal fractal = new Fractal();
            var leavesSprite = FractalTextureGenerator.GenerateSprite(fractal, 48, 48, new Vector2(0.5f, -0.5f));
            this.leavesRenderer.sprite = leavesSprite;
        }
    }
}