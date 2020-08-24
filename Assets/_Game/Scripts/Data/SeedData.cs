namespace TakeCare
{
    public class SeedData : ItemData
    {
        public override void Use(InventorySlot inventorySlot)
        {
            BuildSystem.InitiateSeedPlacement(inventorySlot);    
        }
    }
}