namespace TakeCare
{
    using UnityEngine;
    using UnityEngine.Tilemaps;


    public class PlaceableData : ScriptableObject,
                             IItemData
    {
        public Sprite Icon
        {
            get { return this.Tile.sprite; }
        }


        public void Use()
        {
            
        }


        public Tile Tile;
        public TileBase RequisiteTerrain;
    }
}