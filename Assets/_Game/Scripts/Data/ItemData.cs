namespace TakeCare
{
    using UnityEngine;


    public abstract class ItemData : ScriptableObject
    {
        public Sprite Icon;
        public abstract void Use(InventorySlot inventorySlot);
    }
}