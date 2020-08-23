namespace TakeCare
{
    using Spineless;
    using UnityEngine;
    using UnityEngine.Tilemaps;


    public partial class BuildSystem : MonoBehaviour
    {
        private static BuildSystem _instance;

        public RuleTile GroundTile;

        public Tilemap TerrainTilemap;

        private GameObject previewObject;
        private TileBase originalTile;
        
        public enum BuildState
        {
            Idle,
            PlacingGround
        }


        private SimpleStateMachine<BuildState> state;


        public void Awake()
        {
            _instance = this;
            this.state = new SimpleStateMachine<BuildState>(this);
            this.state.SetState(BuildState.Idle);
        }


        public void Update()
        {
            this.state.Execute();
        }


        public void InitiateGroundPlacement()
        {
            this.state.SetState(BuildState.PlacingGround);
        }


        public void Idle()
        {
            
        }

        public void PlacingGround()
        {
            if (this.previewObject == null)
            {
                this.previewObject = new GameObject("Placement Preview");
                SpriteRenderer spriteRenderer = this.previewObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = this.GroundTile.m_DefaultSprite;
                spriteRenderer.sortingLayerName = SortingLayers.UI;
            }
            
            Vector2 mousePosition = MouseControls.GetCursorWorldPosition();

            this.previewObject.transform.position = mousePosition;

            if (Input.GetButton(InputAxes.Take))
            {
                Vector3Int tilePosition = this.TerrainTilemap.WorldToCell(mousePosition);
                var previousTile = this.TerrainTilemap.GetTile(tilePosition);
                if (previousTile != this.GroundTile)
                {
                    // TODO: Deduct from inventory.
                    this.TerrainTilemap.SetTile(tilePosition, this.GroundTile);    
                }
            }
        }
    }
}

#if UNITY_EDITOR
namespace TakeCare
{
    using UnityEditor;
    using UnityEngine;


    partial class BuildSystem
    {
        [CustomEditor(typeof(BuildSystem))]
        public class BuildSystemEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Place Ground Tile"))
                {
                    ((BuildSystem)this.target).InitiateGroundPlacement();
                }
            }
        }
    }
}
#endif