namespace TakeCare
{
    using Spineless;
    using UnityEngine;
    using UnityEngine.Tilemaps;


    public partial class BuildSystem : MonoBehaviour
    {
        private static BuildSystem instance;

        public RuleTile GroundTile;

        public Tilemap TerrainTilemap;
        public Tilemap FeaturesTilemap;

        private GameObject previewObject;
        private TileBase originalTile;

        private InventorySlot currentItem;
        
        public enum BuildState
        {
            Idle,
            PlacingGround,
            PlacingSeed
        }


        private SimpleStateMachine<BuildState> state;


        public void Awake()
        {
            instance = this;
            this.state = new SimpleStateMachine<BuildState>(this);
            this.state.SetState(BuildState.Idle);
        }


        public void Update()
        {
            this.state.Execute();
        }


        public static void InitiateGroundPlacement(InventorySlot inventorySlot)
        {
            instance.previewObject = new GameObject("Placement Preview");
            SpriteRenderer spriteRenderer = instance.previewObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = instance.GroundTile.m_DefaultSprite;
            spriteRenderer.sortingLayerName = SortingLayers.UI;
            
            instance.state.SetState(BuildState.PlacingGround);
        }


        public static void InitiateSeedPlacement(InventorySlot inventorySlot)
        {
            instance.previewObject = new GameObject("Placement Preview");
            SpriteRenderer spriteRenderer = instance.previewObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = inventorySlot.itemData.Icon;
            spriteRenderer.sortingLayerName = SortingLayers.UI;
            
            instance.state.SetState(BuildState.PlacingSeed);
        }


        public void EndPlacement()
        {
            this.state.SetState(BuildState.Idle);
            GameObject.Destroy(this.previewObject);
        }


        public void Idle()
        {
            
        }

        public void PlacingGround()
        {
            Vector2 mousePosition = MouseControls.GetCursorWorldPosition();
            this.previewObject.transform.position = mousePosition;

            if (Input.GetButton(InputAxes.Take))
            {
                Vector3Int tilePosition = this.TerrainTilemap.WorldToCell(mousePosition);
                var previousTile = this.TerrainTilemap.GetTile(tilePosition);
                if (previousTile != this.GroundTile)
                {
                    bool connects = false;
                    for (int x = tilePosition.x - 1; x < tilePosition.x + 2; x++)
                    {
                        for (int y = tilePosition.y - 1; y < tilePosition.y + 2; y++)
                        {
                            if (x == tilePosition.x
                                && y == tilePosition.y)
                            {
                                continue;
                            }

                            if (this.TerrainTilemap.GetTile(new Vector3Int(x, y, 0)) == this.GroundTile)
                            {
                                connects = true;
                                break;
                            }
                        }
                    }

                    if (connects != true)
                    {
                        Debug.Log("Tile doesn't connect");
                        return;
                    }
                    
                    // TODO: Deduct from inventory.
                    this.TerrainTilemap.SetTile(tilePosition, this.GroundTile);    
                }
            }

            if (Input.GetButton(InputAxes.Care))
            {
                EndPlacement();
            }
        }


        public void PlacingSeed()
        {
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

            if (Input.GetButton(InputAxes.Care))
            {
                EndPlacement();
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
                    InitiateGroundPlacement(null);
                }
            }
        }
    }
}
#endif