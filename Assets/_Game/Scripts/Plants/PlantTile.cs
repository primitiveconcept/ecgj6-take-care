namespace TakeCare
{
    using UnityEngine;
    using UnityEngine.Tilemaps;

    [CreateAssetMenu(fileName = "New Plant Tile", menuName = "Tiles/Plant Tile")]
    public class PlantTile : Tile
    {
        public Plant PlantPrefab;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            base.GetTileData(position, tilemap, ref tileData);
            tileData.gameObject = this.PlantPrefab.gameObject;
            tileData.sprite = this.PlantPrefab.TileSprite;
        }
        
        
    }
}