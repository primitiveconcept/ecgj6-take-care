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

        private PlantResources previousResources;

        public void UpdateScale()
        {
            float totalCarbon =
                this.Resources.Branches.Carbon
                + this.Resources.Leaves.Carbon
                + this.Resources.Features.Carbon;
            totalCarbon = Mathf.Floor(totalCarbon);
            float scale = 1 + totalCarbon;
            this.gameObject.transform.localScale = new Vector3(scale, scale, 1);
        }


        public void UpdateLeaves()
        {
            int spriteIndex = Mathf.RoundToInt(
                (float)Game.LeafSprites.Length 
                * this.Resources.Leaves.Carbon);
            if (spriteIndex >= Game.LeafSprites.Length)
                spriteIndex = Game.LeafSprites.Length - 1;
            if (spriteIndex < 0)
                spriteIndex = 0;
                
            float r = Mathf.Clamp(this.Resources.Leaves.Oxygen, 0, 1);
            float g = Mathf.Clamp(this.Resources.Leaves.Nitrogen, 0, 1);
            float b = Mathf.Clamp(this.Resources.Leaves.Hydrogen, 0, 1);

            this.leavesRenderer.sprite = Game.LeafSprites[spriteIndex];
            Color color = r + g + b > 0.1f
                          ? new Color(r, g, b) 
                          : new Color(0.25f, 0.25f, 0.3f);
            this.leavesRenderer.color = color;
        }


        public void UpdateBranches()
        {
            int spriteIndex = Mathf.RoundToInt(
                (float)Game.BranchSprites.Length 
                * this.Resources.Branches.Carbon);
            if (spriteIndex >= Game.BranchSprites.Length)
                spriteIndex = Game.BranchSprites.Length - 1;
            if (spriteIndex < 0)
                spriteIndex = 0;
                
            float r = Mathf.Clamp(this.Resources.Branches.Oxygen, 0, 1);
            float g = Mathf.Clamp(this.Resources.Branches.Nitrogen, 0, 1);
            float b = Mathf.Clamp(this.Resources.Branches.Hydrogen, 0, 1);

            this.baseRenderer.sprite = Game.BranchSprites[spriteIndex];
            Color color = r + g + b > 0.1f
                              ? new Color(r, g, b) 
                              : new Color(0.3f, 0.25f, 0f);
            this.baseRenderer.color = color;
        }


        public void UpdateFeatures()
        {
            int spriteIndex = Mathf.RoundToInt(
                (float)Game.FeatureSprites.Length 
                * this.Resources.Features.Carbon);
            if (spriteIndex >= Game.FeatureSprites.Length)
                spriteIndex = Game.FeatureSprites.Length - 1;
            if (spriteIndex < 0)
                spriteIndex = 0;
                
            float r = Mathf.Clamp(this.Resources.Features.Oxygen, 0, 1);
            float g = Mathf.Clamp(this.Resources.Features.Nitrogen, 0, 1);
            float b = Mathf.Clamp(this.Resources.Features.Hydrogen, 0, 1);

            this.featureRenderer.sprite = Game.FeatureSprites[spriteIndex];
            Color color = r + g + b > 0.1f
                              ? new Color(r, g, b) 
                              : new Color(0.5f, 0.4f, 0.3f);
            this.featureRenderer.color = color;
        }
        
        
        public void Awake()
        {
            this.baseRenderer = this.Base.GetComponent<SpriteRenderer>();
            this.leavesRenderer = this.Leaves.GetComponent<SpriteRenderer>();
            this.featureRenderer = this.Feature.GetComponent<SpriteRenderer>();
            this.previousResources = this.Resources;
            UpdateScale();
            UpdateLeaves();
            UpdateFeatures();
        }


        public void Update()
        {
            if (!this.Resources.Equals(this.previousResources))
            {
                UpdateScale(); 
                UpdateBranches();
                UpdateLeaves();
                UpdateFeatures();
            }

            this.previousResources = this.Resources;
        }

        public void Start()
        {
            Fractal fractal = new Fractal();
            var leavesSprite = FractalTextureGenerator.GenerateSprite(fractal, 48, 48, new Vector2(0.5f, -0.5f));
            this.leavesRenderer.sprite = leavesSprite;
        }
    }
}