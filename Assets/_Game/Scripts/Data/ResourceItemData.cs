namespace TakeCare
{
    using UnityEngine;

    [CreateAssetMenu()]
    public class ResourceItemData : ItemData
    {
        public ResourceType Type;
        
        public enum ResourceType
        {
            Carbon,
            Oxygen,
            Hydrogen,
            Nitrogen,
            Love
        }
        
        public override void Use(InventorySlot inventorySlot)
        {
            
        }
    }
}